#include "gps.h"
#include "copernicus.h"



float toDegrees(float angle_radians) {
  return 180.0 * angle_radians / M_PI;
}

void Gps::initAll()
{
  Serial.print("I exist");
  gps = CopernicusGPS(1);

}

void Gps::debugAllToConsole() {
  ReportType rpt = gps.processOnePacket();

  // announce any arriving packets to the user
  if (rpt != RPT_NONE) {
    switch (rpt) {
      //  case RPT_ERROR:          Serial.print("ERROR\n"); break;
      //  case RPT_FIX_POS_LLA_32: Serial.print("32 bit lat/lng fix\n"); break;
      //  case RPT_FIX_POS_LLA_64: Serial.print("64 bit lat/lng fix\n"); break;
      //  case RPT_FIX_POS_XYZ_32: Serial.print("32 bit xyz ECEF fix\n"); break;
      //  case RPT_FIX_POS_XYZ_64: Serial.print("64 bit xyz ECEF fix\n"); break;
      //  case RPT_FIX_VEL_ENU:    Serial.print("east/north/up velocity fix\n"); break;
      //  case RPT_FIX_VEL_XYZ:    Serial.print("xyz ECEF velocity fix\n"); break;
      //  case RPT_GPSTIME:
      //           {
      //             Serial.print("gps time ");
      //             Serial.print(gps.getGPSTime().week_no);
      //             Serial.println();
      //             break;
      //           }
      //  case RPT_HEALTH:         Serial.print("receiver health\n"); break;
      //  case RPT_ADDL_STATUS:    Serial.print("receiver additional status\n"); break;
        case RPT_SATELLITES:
        {
          Serial.print("num satellites: ");
          Serial.print(gps.getStatus().n_satellites);
          Serial.println();
          break;
        }
      //  case RPT_SBAS_MODE:      Serial.print("satellite based augmentation report\n"); break;
       default:
        //  Serial.print("unknown packet ");
        //  Serial.print(rpt);
        //  Serial.print("\n");
         break;
    }

    // set the fix mode to 32-bit, if we haven't done so.
    // (arduino support for 64-bit doubles is not available on many boards)
    if (!set_fixmode) {
      gps.setFixMode(RPT_FIX_POS_LLA_32, RPT_FIX_VEL_ENU);
      set_fixmode = true;
    }

    // print the latest fix.
    // LLA measurements are stored in radians.
    if (rpt == RPT_FIX_POS_LLA_32) {
      const LLA_Fix<Float32> *f = gps.getPositionFix().getLLA_32();
      Serial.print("  location: (");
      Serial.print(toDegrees(f->lat.f), 7);
      Serial.print(", ");
      Serial.print(toDegrees(f->lng.f), 7);
      Serial.print(", ");
      Serial.print(f->alt.f);
      Serial.print(") bias: ");
      Serial.print(f->bias.f);
      Serial.print("\n");
    }
  }
}

/**
  * Get the latitude of the rocket
  */
double Gps::getLatitude()
{

};
/**
  * Get the longitude of the rocket
  */
double Gps::getLongitude()
{

};
/**
  * Get the altitude of the rocket
  */
double Gps::getAltitude()
{

};
