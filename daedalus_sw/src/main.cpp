#include "Arduino.h"
#include "imu/imu.h"

Imu imu;

void setup()
{
  // Begin communication on the main serial port for debugging
  Serial.begin(9600);
  imu.initAll();
}

void loop()
{
  imu.debugAllToConsole();
  delay(100);
}
