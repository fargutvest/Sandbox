#include "7segment.h"
void setup() {
  // put your setup code here, to run once:
define_segment_pins(0,1,2,3,4,5,6,7);
define_digit_pins(8,9,10,11, 12, 13, A0, A1);
define_segment_on_off(LOW, HIGH);
define_digit_on_off(HIGH,LOW);

}

int i = 0;
void loop() {
String str = String(i);
print_symbol(str.charAt(0));
dig_all_on();

 i = i + 1;

 if (i == 10)
  i = 0;

 delay(1000);
}
