#include "7segment.h"
#include "time.h"
#include <ThreeWire.h>  
#include <RtcDS1302.h>

ThreeWire myWire(A4,A5,A3); // IO, SCLK, CE
RtcDS1302<ThreeWire> Rtc(myWire);

void setup() {
  // put your setup code here, to run once:
define_segment_pins(0,1,2,3,4,5,6,7);
define_digit_pins(8,9,10,11, 12, 13, A0, A1);
define_segment_on_off(LOW, HIGH);
define_digit_on_off(HIGH,LOW);
}

void loop() {
RtcDateTime now = Rtc.GetDateTime();
seconds = now.Second();
minutes = now.Minute();
hours = now.Hour();
print_seconds();
print_minutes();
print_hours();
}
