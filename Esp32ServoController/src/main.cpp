#include <Arduino.h>
#include <ESP32Servo.h>
#include <vector>
#include <unordered_map>

const int amountOfServos = 18;
const int connectedPins[amountOfServos] = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18};
Servo servos[amountOfServos];
int servoPositions[amountOfServos];
unsigned long servoMoveStartTime[amountOfServos];
unsigned long moveDuration[amountOfServos];

std::unordered_map<int, int> servoPinToIndex;

void setup()
{
  Serial.begin(115200);
  for (int i = 0; i < amountOfServos; i++)
  {
    servos[i].attach(connectedPins[i]);
    servoPositions[i] = 90;
    servos[i].write(90);
    servoMoveStartTime[i] = millis();
    moveDuration[i] = 0;

    // Map servo pin to its index
    servoPinToIndex[connectedPins[i]] = i;
  }
}

struct MotorCommand
{
  int motor;
  int angle;
};

struct FormattedCommandContent
{
  std::vector<MotorCommand> motorCommands;
  int time;
};

// Extracts motor angles and time from "#1A30#5A90T1000"
FormattedCommandContent splitCommandToParts(String str)
{
  std::vector<MotorCommand> motorCommands;
  int time;
  // Holds information what is being parsed ('#', 'A', 'T', _) _ is placeholder and means sign is not assigned
  char sign = '_';
  // Holds numeric string value containing angle or time
  String number;
  // Holds motor number
  String motorNumber;

  for (int i = 0; i < str.length(); i++)
  {
    char c = str[i];

    // Handles first char in str
    if (sign == '_')
    {
      sign = c;
      continue;
    }

    // This is last char, so it must be last digit of time
    if (i == (str.length() - 1))
    {
      number += c;
      time = number.toInt();
      continue;
    }

    if (isdigit(c))
    {
      // Current char is number that is part of the motor number
      if (sign == '#')
      {
        motorNumber += c;
        continue;
      }
      // Current char is number that is part of the angle or time
      number += c;
      continue;
    }

    // At this point c is '#' or 'A'

    // Parsing motor number is finished and now starts parsing of angle
    if (sign == '#')
    {
      sign = 'A';
      continue;
    }

    // Sign was 'A', so now number contains motor angle and motorNumber contains for which motor this angle should be set
    MotorCommand motorCommand;
    motorCommand.motor = motorNumber.toInt();
    motorCommand.angle = number.toInt();
    motorCommands.push_back(motorCommand);

    sign = c;
    motorNumber.clear();
    number.clear();
  }

  FormattedCommandContent content;
  content.motorCommands = motorCommands;
  content.time = time;

  return content;
}

// Accepts commands in form of "#1A30#5A90T1000\n" which means move motor 1 to angle 30 degrees and motor 5 to angle 90 degrees in 1000ms
void handleCommandFromSerial()
{
  if (Serial.available() > 0)
  {
    String command = Serial.readStringUntil('\n');

    FormattedCommandContent formattedCommandContent = splitCommandToParts(command);

    for (MotorCommand motorCommand : formattedCommandContent.motorCommands)
    {
      int index = servoPinToIndex[motorCommand.motor];

      servoPositions[index] = motorCommand.angle;
      servoMoveStartTime[index] = millis();
      moveDuration[index] = formattedCommandContent.time;
    }
  }
}

void loop()
{
  handleCommandFromSerial();

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