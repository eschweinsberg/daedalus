#include "imu.h"

/**
 * Initialize all sensors on the 10dof module
 */
void Imu::initAll()
{
  Serial.println("Initializing Adafruit 10DOF IMU");

  // Create objects for all modules
  dof   = Adafruit_10DOF();
  accel = Adafruit_LSM303_Accel_Unified(30301);
  mag   = Adafruit_LSM303_Mag_Unified(30302);
  bmp   = Adafruit_BMP085_Unified(18001);

  if(!accel.begin())
  {
    /* There was a problem detecting the LSM303 ... check your connections */
    Serial.println(F("Ooops, no LSM303 detected ... Check your wiring!"));
    while(1);
  }
  if(!mag.begin())
  {
    /* There was a problem detecting the LSM303 ... check your connections */
    Serial.println("Ooops, no LSM303 detected ... Check your wiring!");
    while(1);
  }
  if(!bmp.begin())
  {
    /* There was a problem detecting the BMP180 ... check your connections */
    Serial.println("Ooops, no BMP180 detected ... Check your wiring!");
    while(1);
  }

  return;
}

/**
 * Get the pitch of the rocket
 */
double Imu::getPitch()
{
  accel.getEvent(&event_);
  if (dof.accelGetOrientation(&event_, &orientation_))
  {
    return orientation_.pitch;
  }
  else
  {
    Serial.println("Failure retrieving pitch! Disregard result.");
    return ERROR;
  }
}

double Imu::getRoll()
{
  accel.getEvent(&event_);
  if (dof.accelGetOrientation(&event_, &orientation_))
  {
    return orientation_.roll;
  }
  else
  {
    Serial.println("Failure retrieving roll! Disregard result.");
    return ERROR;
  }
}

double Imu::getHeading()
{
  mag.getEvent(&event_);
  if (dof.magGetOrientation(SENSOR_AXIS_Z, &event_, &orientation_))
  {
    return orientation_.heading;
  }
  else
  {
    Serial.println("Failure retrieving heading! Disregard result.");
    return ERROR;
  }
}

double Imu::getAltitude()
{
  return -1;
}

void Imu::debugAllToConsole()
{
  //Serial.print("Pitch: ");
  Serial.println(getPitch());
  //Serial.print(", Roll: ");
  //Serial.print(getRoll());
  //Serial.print(", Heading: ");
  //Serial.println(getHeading());
}
