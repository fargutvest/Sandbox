#include "Arduino.h"
#include "7segment.h"

int A;
int B;
int C;
int D;
int E;
int F;
int G;
int DP;

int D1;
int D2;
int D3;
int D4;
int D5;
int D6;
int D7;
int D8;

bool SEGMENT_ON;
bool SEGMENT_OFF;

bool DIGIT_ON;
bool DIGIT_OFF;

void define_segment_on_off(bool segment_on, bool segment_off){
SEGMENT_ON = segment_on;
SEGMENT_OFF = segment_off;
}

void define_digit_on_off(bool digit_on, bool digit_off){
  DIGIT_ON = digit_on;
  DIGIT_OFF = digit_off;
}

void define_segment_pins(int a, int b, int c, int d, int e, int f, int g, int dp){
  A = a;
  B = b;
  C = c;
  D = d;
  E = e;
  F = f;
  G = g;
  DP = dp;

  pinMode(A, OUTPUT);
  pinMode(B, OUTPUT);
  pinMode(C, OUTPUT);
  pinMode(D, OUTPUT);
  pinMode(E, OUTPUT);
  pinMode(F, OUTPUT);
  pinMode(G, OUTPUT);
  pinMode(DP, OUTPUT);
}

void define_digit_pins(int d1, int d2, int d3, int d4, int d5, int d6, int d7, int d8){
  D1 = d1;
  D2 = d2;
  D3 = d3;
  D4 = d4;
  D5 = d5;
  D6 = d6;
  D7 = d7;
  D8 = d8;

   pinMode(D1, OUTPUT);
   pinMode(D2, OUTPUT);
   pinMode(D3, OUTPUT);
   pinMode(D4, OUTPUT);
   pinMode(D5, OUTPUT);
   pinMode(D6, OUTPUT);
   pinMode(D7, OUTPUT);
   pinMode(D8, OUTPUT);
}

void _1() {
  digitalWrite(A, SEGMENT_OFF);
  digitalWrite(B, SEGMENT_ON);
  digitalWrite(C, SEGMENT_ON);
  digitalWrite(D, SEGMENT_OFF);
  digitalWrite(E, SEGMENT_OFF);
  digitalWrite(F, SEGMENT_OFF);
  digitalWrite(G, SEGMENT_OFF);
  digitalWrite(DP, SEGMENT_OFF);
}

void _2() {
  digitalWrite(A, SEGMENT_ON);
  digitalWrite(B, SEGMENT_ON);
  digitalWrite(C, SEGMENT_OFF);
  digitalWrite(D, SEGMENT_ON);
  digitalWrite(E, SEGMENT_ON);
  digitalWrite(F, SEGMENT_OFF);
  digitalWrite(G, SEGMENT_ON);
  digitalWrite(DP, SEGMENT_OFF);
}

void _3() {
  digitalWrite(A, SEGMENT_ON);
  digitalWrite(B, SEGMENT_ON);
  digitalWrite(C, SEGMENT_ON);
  digitalWrite(D, SEGMENT_ON);
  digitalWrite(E, SEGMENT_OFF);
  digitalWrite(F, SEGMENT_OFF);
  digitalWrite(G, SEGMENT_ON);
  digitalWrite(DP, SEGMENT_OFF);
}

void _4() {
  digitalWrite(A, SEGMENT_OFF);
  digitalWrite(B, SEGMENT_ON);
  digitalWrite(C, SEGMENT_ON);
  digitalWrite(D, SEGMENT_OFF);
  digitalWrite(E, SEGMENT_OFF);
  digitalWrite(F, SEGMENT_ON);
  digitalWrite(G, SEGMENT_ON);
  digitalWrite(DP, SEGMENT_OFF);
}

void _5() {
  digitalWrite(A, SEGMENT_ON);
  digitalWrite(B, SEGMENT_OFF);
  digitalWrite(C, SEGMENT_ON);
  digitalWrite(D, SEGMENT_ON);
  digitalWrite(E, SEGMENT_OFF);
  digitalWrite(F, SEGMENT_ON);
  digitalWrite(G, SEGMENT_ON);
  digitalWrite(DP, SEGMENT_OFF);
}

void _6() {
  digitalWrite(A, SEGMENT_ON);
  digitalWrite(B, SEGMENT_OFF);
  digitalWrite(C, SEGMENT_ON);
  digitalWrite(D, SEGMENT_ON);
  digitalWrite(E, SEGMENT_ON);
  digitalWrite(F, SEGMENT_ON);
  digitalWrite(G, SEGMENT_ON);
  digitalWrite(DP, SEGMENT_OFF);
}

void _7() {
  digitalWrite(A, SEGMENT_ON);
  digitalWrite(B, SEGMENT_ON);
  digitalWrite(C, SEGMENT_ON);
  digitalWrite(D, SEGMENT_OFF);
  digitalWrite(E, SEGMENT_OFF);
  digitalWrite(F, SEGMENT_OFF);
  digitalWrite(G, SEGMENT_OFF);
  digitalWrite(DP, SEGMENT_OFF);
}
void _8() {
  digitalWrite(A, SEGMENT_ON);
  digitalWrite(B, SEGMENT_ON);
  digitalWrite(C, SEGMENT_ON);
  digitalWrite(D, SEGMENT_ON);
  digitalWrite(E, SEGMENT_ON);
  digitalWrite(F, SEGMENT_ON);
  digitalWrite(G, SEGMENT_ON);
  digitalWrite(DP, SEGMENT_OFF);
}
void _9() {
  digitalWrite(A, SEGMENT_ON);
  digitalWrite(B, SEGMENT_ON);
  digitalWrite(C, SEGMENT_ON);
  digitalWrite(D, SEGMENT_ON);
  digitalWrite(E, SEGMENT_OFF);
  digitalWrite(F, SEGMENT_ON);
  digitalWrite(G, SEGMENT_ON);
  digitalWrite(DP, SEGMENT_OFF);
}
void _0() {
  digitalWrite(A, SEGMENT_ON);
  digitalWrite(B, SEGMENT_ON);
  digitalWrite(C, SEGMENT_ON);
  digitalWrite(D, SEGMENT_ON);
  digitalWrite(E, SEGMENT_ON);
  digitalWrite(F, SEGMENT_ON);
  digitalWrite(G, SEGMENT_OFF);
  digitalWrite(DP, SEGMENT_OFF);
}

void clean() {
  digitalWrite(A, SEGMENT_OFF);
  digitalWrite(B, SEGMENT_OFF);
  digitalWrite(C, SEGMENT_OFF);
  digitalWrite(D, SEGMENT_OFF);
  digitalWrite(E, SEGMENT_OFF);
  digitalWrite(F, SEGMENT_OFF);
  digitalWrite(G, SEGMENT_OFF);
  digitalWrite(DP, SEGMENT_OFF);
}

void dig1() {
  digitalWrite(D1, DIGIT_ON);
  digitalWrite(D2, DIGIT_OFF);
  digitalWrite(D3, DIGIT_OFF);
  digitalWrite(D4, DIGIT_OFF);
  digitalWrite(D5, DIGIT_OFF);
  digitalWrite(D6, DIGIT_OFF);
  digitalWrite(D7, DIGIT_OFF);
  digitalWrite(D8, DIGIT_OFF);
}

void dig2() {
  digitalWrite(D1, DIGIT_OFF);
  digitalWrite(D2, DIGIT_ON);
  digitalWrite(D3, DIGIT_OFF);
  digitalWrite(D4, DIGIT_OFF);
  digitalWrite(D5, DIGIT_OFF);
  digitalWrite(D6, DIGIT_OFF);
  digitalWrite(D7, DIGIT_OFF);
  digitalWrite(D8, DIGIT_OFF);
}

void dig3() {
  digitalWrite(D1, DIGIT_OFF);
  digitalWrite(D2, DIGIT_OFF);
  digitalWrite(D3, DIGIT_ON);
  digitalWrite(D4, DIGIT_OFF);
  digitalWrite(D5, DIGIT_OFF);
  digitalWrite(D6, DIGIT_OFF);
  digitalWrite(D7, DIGIT_OFF);
  digitalWrite(D8, DIGIT_OFF);
}

void dig4() {
  digitalWrite(D1, DIGIT_OFF);
  digitalWrite(D2, DIGIT_OFF);
  digitalWrite(D3, DIGIT_OFF);
  digitalWrite(D4, DIGIT_ON);
  digitalWrite(D5, DIGIT_OFF);
  digitalWrite(D6, DIGIT_OFF);
  digitalWrite(D7, DIGIT_OFF);
  digitalWrite(D8, DIGIT_OFF);
}

void dig_all_off() {
  digitalWrite(D1, DIGIT_OFF);
  digitalWrite(D2, DIGIT_OFF);
  digitalWrite(D3, DIGIT_OFF);
  digitalWrite(D4, DIGIT_OFF);
  digitalWrite(D5, DIGIT_OFF);
  digitalWrite(D6, DIGIT_OFF);
  digitalWrite(D7, DIGIT_OFF);
  digitalWrite(D8, DIGIT_OFF);
}

void dig_all_on() {
  digitalWrite(D1, DIGIT_ON);
  digitalWrite(D2, DIGIT_ON);
  digitalWrite(D3, DIGIT_ON);
  digitalWrite(D4, DIGIT_ON);
  digitalWrite(D5, DIGIT_ON);
  digitalWrite(D6, DIGIT_ON);
  digitalWrite(D7, DIGIT_ON);
  digitalWrite(D8, DIGIT_ON);
}

void print_symbol(char symbol) {
  switch (symbol)
  {
    case '0':
      _0();
      break;
    case '1':
      _1();
      break;
    case '2':
      _2();
      break;
    case '3':
      _3();
      break;
    case '4':
      _4();
      break;
    case '5':
      _5();
      break;
    case '6':
      _6();
      break;
    case '7':
      _7();
      break;
    case '8':
      _8();
      break;
    case '9':
      _9();
  }
}