#include <Arduino.h>
#include <ESP32Servo.h>

const int amountOfServos = 3;
Servo servos[amountOfServos];
int servoPositions[amountOfServos];
unsigned long servoMoveStartTime[amountOfServos];
unsigned long moveDuration[amountOfServos];

void setup()
{
  Serial.begin(115200);
  for (int i = 0; i < amountOfServos; i++)
  {
    servos[i].attach(i + 2);
    servoPositions[i] = 90;
    servos[i].write(90);
    servoMoveStartTime[i] = millis();
    moveDuration[i] = 0;
  }
}

void readCommandFromSerial()
{
  if (Serial.available() > 0)
  {
    String command = Serial.readStringUntil('\n');

    int numbers[3];
    String currentNumber = "";
    int currentIndex = 0;

    for (size_t i = 1; i < command.length(); i++)
    {
      char c = command[i];

      if (isdigit(c))
      {
        currentNumber += c;
      }
      else
      {
        numbers[currentIndex] = currentNumber.toInt();
        currentIndex += 1;
        currentNumber.clear();
      }
    }

    int servoNumber = numbers[0];
    int targetPosition = numbers[1];
    int newMoveDuration = numbers[2];

    servoPositions[servoNumber] = targetPosition;
    servoMoveStartTime[servoNumber] = millis();
    moveDuration[servoNumber] = newMoveDuration;
  }
}

void loop()
{
  readCommandFromSerial();

  for (int i = 0; i < amountOfServos; i++)
  {
    unsigned long currentTime = millis();
    if (moveDuration[i] > 0 && currentTime - servoMoveStartTime[i] <= moveDuration[i])
    {
      int startPosition = servos[i].read();
      int targetPosition = servoPositions[i];
      float progress = static_cast<float>(currentTime - servoMoveStartTime[i]) / moveDuration[i];
      int newPosition = startPosition + (targetPosition - startPosition) * progress;
      servos[i].write(newPosition);
    }
    else
    {
      moveDuration[i] = 0;
    }
  }
}