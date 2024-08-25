#include "7segment.h"
void setup() {
  // put your setup code here, to run once:
define_segment_pins(12,1,2,3,4,5,6,7);
define_digits(8,9,10,11);
define_segment_on_off(LOW, HIGH);
define_digit_on_off(HIGH,LOW);

}

int i = 0;
void loop() {
String str = String(i);
print_symbol(str.charAt(0));
dig1();

 i = i + 1;

 if (i == 10)
  i = 0;

 delay(1000);
}
