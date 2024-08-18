#include "Arduino.h"
#include "7segment.h"
#include "symbols.h"
#include "digs.h"
#include <string.h>

int seconds = 0;
int minutes = 0;
int hours = 0;
int time_counter = 0;
int display_refresh_period_ms = 50;
int tick_seconds_period_ms = 1000;

void tick_hours() {
  if (seconds == 23) {
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

    printSymbol(str.charAt(0));
    dig2();
    dig_all_off();
    printSymbol(str.charAt(1));
    dig1();
    dig_all_off();
  }
}

void print_minutes() {
  if (minutes <= 59 && minutes >= 0) {
    String str = String(minutes);
    if (str.length() == 1)
      str = "0" + str;

    printSymbol(str.charAt(0));
    dig5();
    dig_all_off();
    printSymbol(str.charAt(1));
    dig4();
    dig_all_off();
  }
}

void print_hours() {
  if (hours <= 23 && hours >= 0) {
    String str = String(hours);
    if (str.length() == 1)
      str = "0" + str;

    printSymbol(str.charAt(0));
    dig8();
    dig_all_off();
    printSymbol(str.charAt(1));
    dig7();
    dig_all_off();
  }
}

void setup() {
  define_pins();
}


void loop() {
  delay(display_refresh_period_ms);

  time_counter += display_refresh_period_ms;

  if (time_counter == tick_seconds_period_ms) {
    time_counter = 0;
    tick_seconds();
  }


  print_seconds();
  dash();
  dig3();
  dig_all_off();
  print_minutes();
  dash();
  dig6();
  dig_all_off();
  print_hours();
}



