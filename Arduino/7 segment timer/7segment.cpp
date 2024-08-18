#include "Arduino.h"
#include "7segment.h"
#include "symbols.h"
#include "digs.h"

void define_pins() {
  pinMode(A, OUTPUT);
  pinMode(B, OUTPUT);
  pinMode(C, OUTPUT);
  pinMode(D, OUTPUT);
  pinMode(E, OUTPUT);
  pinMode(F, OUTPUT);
  pinMode(G, OUTPUT);

  pinMode(D1, OUTPUT);
  pinMode(D2, OUTPUT);
  pinMode(D3, OUTPUT);
  pinMode(D4, OUTPUT);
  pinMode(D5, OUTPUT);
  pinMode(D6, OUTPUT);
  pinMode(D7, OUTPUT);
  pinMode(D8, OUTPUT);
}

void printSymbol(char symbol) {
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