
int RPWM = 10;  //connect Arduino pin 10 to IBT-2 pin RPWM   down
int LPWM = 9;  //connect Arduino pin 9 to IBT-2 pin LPWM        up
bool A;
void setup() {

  pinMode(9, OUTPUT);
  pinMode(10, OUTPUT);

  analogWrite(RPWM, 250);
  analogWrite(LPWM,0 );
  delay(1500); 

  analogWrite(LPWM, 0);
  analogWrite(RPWM, 0);
  delay(7000);
  A = true;
}



void loop() {
  if (A == true)
  {
      analogWrite(LPWM, 250);
      delay(1000);
      analogWrite(LPWM, 0);
  }
  
  A = false;
}



