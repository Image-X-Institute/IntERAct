volatile byte Speed = 0; // Intialize Varaible for the speed of the motor (0-255);
volatile bool newData = false; //Initialise bool to check if new data has arrived
// volatile String receivedStr; //Initialise String that represents data received via serial port for encoder
String receivedStr;
float conNum = 0.4816955684;
int RPWM = 10;  //connect Arduino pin 10 to IBT-2 pin RPWM
int LPWM = 9;  //connect Arduino pin 9 to IBT-2 pin LPWM
float speedInput;
int trigDelay = 1750;        
bool Stop = false;

volatile float voltage;  //Initialise voltage float
volatile float delay_time; //Initialise delay float (how long the motor will be moved for given voltage)
volatile bool up;  // Initialise bool to check if motor should move up or down

volatile long prevpos = 0;
volatile long pos = 0; 
volatile float posInmm = 0;                  // Actuator Position in Pulses

volatile unsigned long lastStepTime = 0; // Time stamp of last pulse

volatile bool reached = false;  //self-calibration of movement
volatile float destinationPosition = 0;

volatile bool newrun = 1;


void setup() {

  Serial.begin(115200);
  pinMode(10, OUTPUT); // Configure pin 10 as an Output
  pinMode(9, OUTPUT); // Configure pin 11 as an Output
  Speed = 255;

  pinMode(2, INPUT_PULLUP);
  // attachInterrupt(digitalPinToInterrupt(2), countSteps, RISING); //count pulses for feedback

  // Move motor to base position
  analogWrite(RPWM, Speed);
  analogWrite(LPWM, 0);
  delay(6000); 

  analogWrite(RPWM, 0);
  analogWrite(LPWM, 0);
  delay(1000);


}

int i = 0;
void loop() {
  if (Serial.available() > 0 && newData == false) { //Check if Serial connection is available and newData is false

    receivedStr = Serial.readStringUntil('\n'); //Read data from serial port
    
    int firstCommaIndex = receivedStr.indexOf(',');
    String voltageStr = receivedStr.substring(0, firstCommaIndex);
    voltage = voltageStr.toFloat(); //Split the string by ',' delimeter and allocate first section of string as voltage
    
    int secondCommaIndex = receivedStr.indexOf(',', firstCommaIndex + 1);
    String upStr = receivedStr.substring(firstCommaIndex + 1, secondCommaIndex);
    up = (upStr == "1");  //Split the string by ',' delimeter and allocate second section of string as up bool

    int thirdCommaIndex = receivedStr.indexOf(',', secondCommaIndex + 1);
    String delayStr = receivedStr.substring(secondCommaIndex + 1, thirdCommaIndex);
    delay_time = delayStr.toFloat(); //Split the string by ',' delimeter and allocate third section of string as delay value

    String DestinationStr = receivedStr.substring(thirdCommaIndex + 1);
    destinationPosition = DestinationStr.toFloat();


    if (Stop == true)
    {
      voltage =0;
      delay_time = 0;
      Stop = false;
    }
    //////// Using mapping to determine voltage
    // if (up == true)
    // {
    //   voltage = map(voltage, 0, 62.75, 0, 255);
    // }
    // else if (up == false)
    // {
    //   voltage = map(voltage, 0, 64.625, 0, 255);
    // }

    // if (voltage < 45)
    // {
    //   voltage  = 55;
    // }
    //////// Uncomment section to enable

    if (receivedStr == "Home") { //Check if Home button was pressed on Winforms application and send motor to base position
      // Return motor to start position
      analogWrite(RPWM, 165); // Stop RPWM
      analogWrite(LPWM, 0); // Move LPWM in the reverse direction



      delay(5000); // Wait for the motor to return to start position
      
      
      analogWrite(LPWM, 0); // Stop LPWM

      pos = 0; //Reset position and steps to 0
      posInmm = 0; //Reset position and steps to 0
      lastStepTime = 0; //Reset position and steps to 0
      newData = false; // Reset newData flag
      newrun = true;
      reached = false;
      return; // Exit loop() to prevent further execution
    }
  
    newData = true; // Once data batch has been received, set the newData flag to true to process values and move motor
  }

  if (newData == true) // Check if data has arrived, move motor accordingly.
  {


    if (receivedStr == "Stop")
    {
    analogWrite(RPWM, 0); // Stop RPWM
    analogWrite(LPWM, 0); // Move LPWM in the reverse direction
    Stop = true;


    newData = true;     


    }

      //if use encoder

      // bool flag = 0;
      // if (up){
      //   reached = (posInmm>=destinationPosition);
      // }else{
      //   reached = (posInmm<=destinationPosition);
      // }
      // while(reached==false){
      //   if (up == true) { //if Up is true, move motor up with given voltage and delay
      //     analogWrite(RPWM, 0);
      //     analogWrite(LPWM, voltage);
      //   } else {          //if Up is false, move motor down with given voltage and delay
      //     analogWrite(RPWM, voltage);
      //     analogWrite(LPWM, 0);
      //   }
      //   flag=1;
      // }
      // analogWrite(LPWM, 0);
      // analogWrite(RPWM, 0);

    
      //
      //if not using encoder
        if (up == true) { //if Up is true, move motor up with given voltage and delay
          analogWrite(RPWM, 0);
          analogWrite(LPWM, voltage);
        } else {          //if Up is false, move motor down with given voltage and delay
          analogWrite(RPWM, voltage);
          analogWrite(LPWM, 0);
        }
    
        delay(delay_time);


        if (100-delay_time > 0){
          analogWrite(RPWM,0);
          analogWrite(LPWM, 0);
          delay(100 - delay_time);
        }
      //

    //updatePosition();  // Call function to count pulses and conver to position in pulses
     // Relay positional feedback in mm to application
    // Serial.println("ack:"+String(destinationPosition)+","+String(posInmm)+","+reached); // Send acknowledgment of received data
    // Serial.println("ack:"+ receivedStr); // Send acknowledgment of received data
    newData = false; //Once data has been processed, flag newData as false to allow receival of data again.
  }
}


// void countSteps(void) { // This function counts steps by pulses detected in interrupt pin
//   if(micros()-lastStepTime > trigDelay){
//     if(up == true){
//       pos ++;
//     } else if (up == false) {
//       pos --;
//     }
//     posInmm= conNum*pos;

//     if (up){
//       reached = (posInmm>=destinationPosition);
//     }else{
//       reached = (posInmm<=destinationPosition);
//       if(posInmm<=0.5){
//         reached =true;

//       };
//     }
//     lastStepTime = micros();
//   }
// }


