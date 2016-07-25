#include <TinyGPS.h>
#include "imu/imu.h"
#include <Adafruit_Sensor.h>
#include <Adafruit_LSM303_U.h>
#include <Wire.h>
#include <SD.h>
#include <SPI.h>

TinyGPS gps;
//Imu imu;
String Stage = "P";
//Adafruit_LSM303_Accel_Unified accel = Adafruit_LSM303_Accel_Unified(54321);
File myFile;
unsigned long time;

void setup(){
  Serial.begin(115200); // Debugger
  Serial2.begin(57600); // Xbee
  Serial3.begin(4800);  // GPS
  Serial.println("Preparing GPS...");
  Serial.println("Preparing IMU...");
  //imu.initAll();  // Altimeter/Gyroscope
  // if(!accel.begin()){ // Accelerometer
  //   Serial2.println("Problem detecting the LSM303...");
  //   Serial.println("Problem detecting the LSM303...");
  // }
  pinMode(53, OUTPUT);
  if (!SD.begin()) {
    Serial.println("SD initialization failed!");
    return;
  }
}

void loop()
{
  bool newData = false;
  unsigned long chars;
  unsigned short sentences, failed;
  myFile = SD.open("Payload.txt", FILE_WRITE);
  time = millis();
  // For one second we parse GPS data and report some key values
  for (unsigned long start = millis(); millis() - start < 1000;)
  {
    while (Serial3.available())
    {
      char c = Serial3.read();
      // Serial.write(c); // uncomment this line if you want to see the GPS data flowing
      if (gps.encode(c)) // Did a new valid sentence come in?
        newData = true;
    }
  }

  if (newData)
  {
    // ONLY in the event of new GPS data would the device print gps data.....
    float flat, flon;
    unsigned long age;
    gps.f_get_position(&flat, &flon, &age);

    Serial2.print(time);
    Serial2.print(",");
    Serial2.print(flat == TinyGPS::GPS_INVALID_F_ANGLE ? 0.0 : flat, 6);
    Serial2.print(",");
    Serial2.print(flon == TinyGPS::GPS_INVALID_F_ANGLE ? 0.0 : flon, 6);
    Serial2.print(",");
    Serial2.print(gps.f_altitude());

    if(myFile){
      myFile.print(time);
      myFile.print(",");
      myFile.print(flat == TinyGPS::GPS_INVALID_F_ANGLE ? 0.0 : flat, 6);
      myFile.print(",");
      myFile.print(flon == TinyGPS::GPS_INVALID_F_ANGLE ? 0.0 : flon, 6);
      myFile.print(",");
      myFile.print(gps.f_altitude());
    }
    else{
      Serial.println("Error printing to txt file!!");
    }

    // Debugging purposes.
    Serial.print(time);
    Serial.print(",");
    Serial.print(flat == TinyGPS::GPS_INVALID_F_ANGLE ? 0.0 : flat, 6);
    Serial.print(",");
    Serial.print(flon == TinyGPS::GPS_INVALID_F_ANGLE ? 0.0 : flon, 6);
    Serial.print(",");
    Serial.print(gps.f_altitude());
  }
  gps.stats(&chars, &sentences, &failed);
  if (chars == 0)
    Serial.println("** No characters received from GPS: check wiring **");

  //sensors_event_t event;
  //accel.getEvent(&event);


  // Serial2.print(imu.getPitch());
  // Serial2.print(",");
  // Serial2.print(imu.getRoll());
  // Serial2.print(",");
  // Serial2.print(imu.getHeading());
  // Serial2.print(",");
  // Serial2.print(event.acceleration.x);
  // Serial2.print(",");
  // Serial2.print(event.acceleration.y);
  // Serial2.print(",");
  // Serial2.print(event.acceleration.z);

  // if(myFile){
  //   myFile.print(imu.getPitch());
  //   myFile.print(",");
  //   myFile.print(imu.getRoll());
  //   myFile.print(",");
  //   myFile.print(imu.getHeading());
  //   myFile.print(",");
  //   myFile.print(event.acceleration.x);
  //   myFile.print(",");
  //   myFile.print(event.acceleration.y);
  //   myFile.print(",");
  //   myFile.print(event.acceleration.z);
  // }
  // else{
  //   Serial.println("Error printing to txt file!!");
  // }

  // Serial.print(imu.getPitch());
  // Serial.print(",");
  // Serial.print(imu.getRoll());
  // Serial.print(",");
  // Serial.print(imu.getHeading());
  // Serial.print(",");
  // Serial.print(event.acceleration.x);
  // Serial.print(",");
  // Serial.print(event.acceleration.y);
  // Serial.print(",");
  // Serial.print(event.acceleration.z);

  Serial2.println("");  //End both pieces...
  Serial.println("");
  myFile.println("");
  myFile.close();
}

// Actual code....
//
// #include "Arduino.h"
// #include "imu/imu.h"
// #include "gps/gps.h"
// #include "sd.h"
// #include "spi.h"
//
// Imu imu;
// Gps gps;
// File myFile;
//
// void printPacket();
// void writeToCard();
// char stage = 'P';
//
// void setup()
// {
//   // Begin communication on the main serial port for debugging
//   Serial.begin(9600); // Debugger;
//   Serial2.begin(57600); // XBee
//   Serial3.begin(4800); // GPS
//   while(!Serial);
//
//   //imu.initAll();
//   Serial.println("Start doing shit.");
//   gps.initAll();
//   Serial.println("GPS and IMU initialized.");
//
//   Serial.begin(9600);
//   Serial.print("Initializing SD card...");
// // On the Ethernet Shield, CS is pin 4. It's set as an output by default.
// // Note that even if it's not used as the CS pin, the hardware SS pin
// // (10 on most Arduino boards, 53 on the Mega) must be left as an output
// // or the SD library functions will not work.
//   pinMode(53, OUTPUT);
//
//   if (!SD.begin(10)) {
//     Serial.println("initialization failed!");
//     return;
//   }
//   Serial.println("initialization done.");
// }
//
// void loop()
// {
//   printPacket();
//   writeToCard();
//   delay(250);
// }
//
// void printPacket()
// {
//   // Investigate retreiving G forces from the IMU also... Place them after the GPS.
//   // Serial2.print(stage + ",");
//   // Serial2.print(imu.getAltitude());
//   // Serial2.print(",");
//   // Serial2.print(imu.getHeading());
//   // Serial2.print(",");
//   // Serial2.print(imu.getPitch());
//   // Serial2.print(",");
//   // Serial2.print(imu.getRoll());
//   // Serial2.print(",");
//   // Serial2.print(gps.getAltitude());
//   // Serial2.print(",");
//   // Serial2.print(gps.getLatitude());
//   // Serial2.print(",");
//   // Serial2.print(gps.getLongitude());
//   // Serial2.print(",");
//   // Serial2.println(gps.getTime());
//
//   // Debugging purposes.
//   Serial.print(stage + ",");
//   // Serial.print(imu.getAltitude());
//   // Serial.print(",");
//   // Serial.print(imu.getHeading());
//   // Serial.print(",");
//   // Serial.print(imu.getPitch());
//   // Serial.print(",");
//   // Serial.print(imu.getRoll());
//   // Serial.print(",");
//   Serial.print(gps.getAltitude());
//   Serial.print(",");
//   Serial.print(gps.getLatitude());
//   Serial.print(",");
//   Serial.print(gps.getLongitude());
//   Serial.print(",");
//   Serial.println(gps.getTime());
// }
//
// void writeToCard()
// {
//    myFile = SD.open("Payload.txt", FILE_WRITE);
//    if (myFile) {
//      Serial.print("Attempting to write to test.txt...");
//      myFile.print(stage + ",");
//     //  myFile.print(imu.getAltitude());
//     //  myFile.print(",");
//     //  myFile.print(imu.getHeading());
//     //  myFile.print(",");
//     //  myFile.print(imu.getPitch());
//     //  myFile.print(",");
//     //  myFile.print(imu.getRoll());
//     //  myFile.print(",");
//      myFile.print(gps.getAltitude());
//      myFile.print(",");
//      myFile.print(gps.getLatitude());
//      myFile.print(",");
//      myFile.print(gps.getLongitude());
//      myFile.print(",");
//      myFile.println(gps.getTime());
//      myFile.close();
//      Serial.println("Printed succesfully!");
//    } else {
//      // if the file didn't open, print an error:
//      Serial.println("error opening test.txt");
//    }
// }
//
// // if (Serial.available())
// // { // If data comes in from serial monitor, send it out to XBee
// //   Serial3.write(Serial.read());
// // }
// // if (Serial3.available())
// // { // If data comes in from XBee, send it out to serial monitor
// //   Serial.write(Serial3.read());
// // }
// // gps.processNextPacket();
// // //Serial3.println("1,12000,31.56778,34.56936"); // Test Packet
// // Serial3.print("1,");
// // Serial3.print(gps.getAltitude());
// // Serial3.print(",");
// // Serial3.print(gps.getLatitude());
// // Serial3.print(",");
// // Serial3.println(gps.getLongitude());
