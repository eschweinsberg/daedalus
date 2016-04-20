/**
 * Property of The University of Akron - Rocket Design Team
 *
 * DESCRIPTION: Helper library for simplifying getting values from the Copernicus
 * II GPS
 *
 * AUTHOR:
 */

#include <copernicus.h>

class Gps
{
public:
  const unsigned char HEALTHY = 0x0;
  void initAll();
  void processNextPacket();
  void debugAllToConsole();
  float getLatitude();
  float getLongitude();
  float getAltitude();
  int getHealth();
  int getTime();

private:
  CopernicusGPS gps_;
  float lat_;
  float long_;
  float alt_;
  float bias_;
  int health_;
  int time_;
};
