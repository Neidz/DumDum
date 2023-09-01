# Project Overview
Welcome to the project DumDum. This is my second take on 3d printed hexapod robot, this time more robust implementation with hopefully better code (not hard to beat last one), stronger motors and more flexible 3d model.

Original hexapod can be found [here](https://github.com/Neidz/Hexapod). Let's just say the code was not perfect.

This project is still in early stages but if you want to take a look at development then go ahead.

# Granting User Permission to Access the Serial Port (Linux)

## Temporary solution for giving permission to access port
You can grant temporary access to the serial port using the following command. Replace `/dev/ttyACM0` with the actual path to your serial port:

```bash
`sudo chmod a+rw /dev/ttyACM0`
```

**Caution**: While this command provides quick access, it grants broad read and write permissions to all users, which can have security implications. It's recommended to use the persistent solution for a more secure setup.

## Persistent solution
To ensure persistent permissions for your serial port, follow these steps:
1. Create a new udev rule file for your device using a text editor. You may need superuser privileges to do this:

```bash
sudo nano /etc/udev/rules.d/99-usb-serial.rules
```

2. Add the following rule to the file, replacing your_vendor_id and your_product_id with the Vendor ID and Product ID of your USB device. You can find these values by running lsusb and looking for your device's entry:

```bash
SUBSYSTEM=="tty", ATTRS{idVendor}=="your_vendor_id", ATTRS{idProduct}=="your_product_id", MODE="0666"
```

3. Save the file and exit the text editor.
4. Reload the udev rules to apply the changes:

```bash
sudo udevadm control --reload-rules
```

With these steps completed, your device should have the necessary permissions to access the serial port without requiring manual adjustments every time it's connected.