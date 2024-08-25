extern int A;
extern int B;
extern int C;
extern int D;
extern int E;
extern int F;
extern int G;
extern int DP;

extern bool SEGMENT_ON;
extern bool SEGMENT_OFF;

extern bool DIGIT_ON;
extern bool DIGIT_OFF;

extern int D1;
extern int D2;
extern int D3;
extern int D4; 

void define_segment_pins(int a, int b, int c, int d, int e, int f, int g, int dp);
void define_digits(int d1, int d2, int d3, int d4);
void define_segment_on_off(bool segment_on, bool segment_off);
void define_digit_on_off(bool digit_on, bool digit_off);

void _0();
void _1();
void _2();
void _3();
void _4();
void _5();
void _6();
void _7();
void _8();
void _9();
void clean();

void dig1();
void dig2();
void dig3();
void dig4();

void print_symbol(char symbol);