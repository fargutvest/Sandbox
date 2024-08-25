#include "7segment.h"
#include "time.h"

void setup() {
  // put your setup code here, to run once:
define_segment_pins(0,1,2,3,4,5,6,7);
define_digit_pins(8,9,10,11, 12, 13, A0, A1);
define_segment_on_off(LOW, HIGH);
define_digit_on_off(HIGH,LOW);
}

void loop() {
tick_seconds();
print_seconds();
print_minutes();
print_hours();
}
