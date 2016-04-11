/**
 * Property of The University of Akron - Rocket Design Team
 *
 * DESCRIPTION: Helper library for simplifying getting values from the Micro-
 * SD Breakout board
 *
 * AUTHOR: Nana Anim
 */

 class Sd
 {
 public:
   const double ERROR = -1000000;

   void initAll();
   void debugAllToConsole();
   double getFile();
   double getData();

 };
