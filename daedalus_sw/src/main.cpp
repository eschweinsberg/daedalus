#include "Arduino.h"
#include "imu/imu.h"
#include "gps/gps.h"

Imu imu;
Gps gps;

void setup()
{
  // Begin communication on the main serial port for debugging
  Serial.begin(9600);
  while(!Serial);
//  imu.initAll();
  gps.initAll();
}

void loop()
{
//  imu.debugAllToConsole();
  gps.debugAllToConsole();
  delay(100);
}
