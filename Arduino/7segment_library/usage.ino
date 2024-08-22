#include "7segment.h"
void setup() {
  // put your setup code here, to run once:
define_segment_pins(12,1,2,3,4,5,6,7);
define_digits(8,9,10,11);
define_segment_on_off(LOW, HIGH);
define_digit_on_off(HIGH,LOW);

}

void loop() {
  // put your main code here, to run repeatedly:
 _1();
 dig3();
}
