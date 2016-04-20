#include "Arduino.h"
#include "imu/imu.h"
#include "gps/gps.h"
#include "sd/sd.h"

Imu imu;
Gps gps;
Sd sd;

void setup()
{
  // Begin communication on the main serial port for debugging
  Serial.begin(9600);
  while(!Serial);

  // imu.initAll();
  gps.initAll();
}

void loop()
{
  gps.processNextPacket();
  Serial.print(gps.getLatitude());
  delay(500);
}
