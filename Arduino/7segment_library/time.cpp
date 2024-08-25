#include "time.h"
#include "7segment.h"
#include "Arduino.h"

int seconds = 0;
int minutes = 0;
int hours = 0;

int pulse_width_modulation_delay_microseconds = 2000;

void tick_hours() {
  if (hours == 23) {
    hours = 0;
  }
  else {
    hours += 1;
  }
}

void tick_minutes() {
  if (minutes == 59) {
    minutes = 0;
    tick_hours();
  }
  else {
    minutes += 1;
  }
}

void tick_seconds() {
  if (seconds == 59) {
    seconds = 0;
    tick_minutes();
  }
  else {
    seconds += 1;
  }
}

void print_seconds() {
  if (seconds <= 59 && seconds >= 0) {
    String str = String(seconds);
    if (str.length() == 1)
      str = "0" + str;

    print_symbol(str.charAt(0));
    dig2();
    delayMicroseconds(pulse_width_modulation_delay_microseconds);
    dig_all_off();
    print_symbol(str.charAt(1));
    dig1();
    delayMicroseconds(pulse_width_modulation_delay_microseconds);
    dig_all_off();
  }
}

void print_minutes() {
  if (minutes <= 59 && minutes >= 0) {
    String str = String(minutes);
    if (str.length() == 1)
      str = "0" + str;

    print_symbol(str.charAt(0));
    dig4();
    delayMicroseconds(pulse_width_modulation_delay_microseconds);
    dig_all_off();
    print_symbol(str.charAt(1));
    dig3();
    delayMicroseconds(pulse_width_modulation_delay_microseconds);
    dig_all_off();
  }
}

void print_hours() {
  if (hours <= 23 && hours >= 0) {
    String str = String(hours);
    if (str.length() == 1)
      str = "0" + str;

    print_symbol(str.charAt(0));
    dig6();
    delayMicroseconds(pulse_width_modulation_delay_microseconds);
    dig_all_off();
    print_symbol(str.charAt(1));
    dig5();
    delayMicroseconds(pulse_width_modulation_delay_microseconds);
    dig_all_off();
  }
}