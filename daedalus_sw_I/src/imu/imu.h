/**
 * Property of The University of Akron - Rocket Design Team
 *
 * DESCRIPTION: Helper library for simplifying getting values from the Adafruit
 * 10DOF IMU
 *
 * AUTHOR: Ethan Schweinsberg
 */

#include <Adafruit_10DOF.h>

class Imu
{
  public:
    const double ERROR = -1000000;

    void initAll();
    void debugAllToConsole();
    double getPitch();
    double getRoll();
    double getHeading();
    double getAltitude();

    double getMedian(int n);

  private:
    Adafruit_10DOF                dof;
    Adafruit_LSM303_Accel_Unified accel;
    Adafruit_LSM303_Mag_Unified   mag;
    Adafruit_BMP085_Unified       bmp;

    sensors_event_t event_;
    sensors_vec_t orientation_;
    sensors_axis_t axis_;

};
