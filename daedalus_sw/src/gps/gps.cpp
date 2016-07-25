#include "gps.h"
#include "copernicus.h"

/**
 * Converts radians to degrees
 * @param  angle_radians
 * @return
 */
float toDegrees(float angle_radians) {
  return 180.0 * angle_radians / M_PI;
}

/**
 * Initialize the GPS repository
 */
void Gps::initAll()
{
  Serial.println("Attempting to acquire satellites.");
  gps_ = CopernicusGPS(1);
  gps_.setFixMode(RPT_FIX_POS_LLA_32, RPT_FIX_VEL_ENU);
  alt_ = 0;
  long_ = 0;
  lat_ = 0;
  bias_ = 0;
  time_ = 0;
  Serial.println("Satellites acquired.");
}

/**
 * Processes the next packet from the GPS and updates all of the metrics
 */
void Gps::processNextPacket()
{
  ReportType rpt = gps_.processOnePacket();

  if (rpt != RPT_NONE)
  {
    switch(rpt)
    {
      case RPT_FIX_POS_LLA_32:
      {
        const LLA_Fix<Float32> *f = gps_.getPositionFix().getLLA_32();
        lat_ = f->lat.f;
        long_ = f->lng.f;
        alt_ = f->lat.f;
        bias_ = f->bias.f;
        break;
      }
      case RPT_HEALTH:
      {
        health_ = gps_.getStatus().health;
        break;
      }
      case RPT_GPSTIME:
      {
        time_ = gps_.getGPSTime().time_of_week.f;
        break;
      }
      default:
      {
        break;
      }
    }
  }
}

/**
  * Get the latitude of the rocket
  */
float Gps::getLatitude()
{
  return lat_;
};
/**
  * Get the longitude of the rocket
  */
float Gps::getLongitude()
{
  return long_;
};
/**
  * Get the altitude of the rocket
  */
float Gps::getAltitude()
{
  return alt_;
};
/**
 * Get the health of the gps receiver
 */
int Gps::getHealth()
{
  return health_;
}
/**
 * Get the current time
 */
int Gps::getTime()
{
  return time_;
}
