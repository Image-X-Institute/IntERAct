//This code needs to be run if the voltage-motor speed relationship needs to be different because of the configuration used. 
int RPWM = 10;  //Makes the actuator going down
int LPWM = 9;  //Makes the actuator going up
bool A;
void setup() {

  pinMode(9, OUTPUT);
  pinMode(10, OUTPUT);


  //makes the actuator going to the fully retracted position for going up measurements. For going down measurements swap RPWM and LPWM. 
  analogWrite(RPWM, 250); 
  analogWrite(LPWM,0 );
  delay(1500); 
  

  //Waits 7 seconds after mooving the actuator to the home position (full retracted for going up tests or fully extended for going down tests) 
  analogWrite(LPWM, 0);
  analogWrite(RPWM, 0);
  delay(7000);
  A = true;
}



void loop() {
  if (A == true)
  {
      //makes the actutor mooving with V=250 for 1s
      //swap LPWM and RPWM for going down measurements
      analogWrite(LPWM, 250);
      delay(1000);
      analogWrite(LPWM, 0);
  }
  
  A = false;
}



