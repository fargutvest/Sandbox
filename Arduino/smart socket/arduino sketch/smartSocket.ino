void setup() {
  Serial.begin(9600);
  pinMode(13, OUTPUT);
  
}
int pinLevel = 255;

void loop() {
  Serial.write(pinLevel);
  while(Serial.available() == 0){
   delay(10);
  }
  pinLevel = Serial.read();
  digitalWrite(13, pinLevel);    
}