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
  const double ERROR = -1000000;

  void initAll();
  void debugAllToConsole();
  double getLatitude();
  double getLongitude();
  double getAltitude();

private:
  CopernicusGPS gps;
  bool set_fixmode = false;

};
