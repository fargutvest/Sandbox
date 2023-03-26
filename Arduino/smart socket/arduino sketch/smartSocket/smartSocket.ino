int pinLevel = 255;

void setup() {
  Serial.begin(9600);
  pinMode(13, OUTPUT);
}


void loop() {
  if (Serial.available() > 0){
  pinLevel = Serial.read(); 
  } 
  digitalWrite(13, pinLevel); 
  Serial.write(pinLevel);
  delay(500);
}
