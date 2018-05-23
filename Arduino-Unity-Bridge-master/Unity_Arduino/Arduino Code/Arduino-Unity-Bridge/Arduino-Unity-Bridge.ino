// Some variables we want to send to Unity
int someInt = 128;
float someFloat = 512.256f;
*char someString = "astring";

const int FSR_PIN = A0; // Pin connected to FSR/resistor divider

// Measure the voltage at 5V and resistance of your 3.3k resistor, and enter
// their value's below:
const float VCC = 4.98; // Measured voltage of Ardunio 5V line
const float R_DIV = 3230.0; // Measured resistance of 3.3k resistor

void setup() 
{
  Serial.begin(9600);
  pinMode(FSR_PIN, INPUT);
}

void loop() 
{
  int fsrADC = analogRead(FSR_PIN);
  // If the FSR has no pressure, the resistance will be
  // near infinite. So the voltage should be near 0.
  if (fsrADC != 0) // If the analog reading is non-zero
  {
    // Use ADC reading to calculate voltage:
    float fsrV = fsrADC * VCC / 1023.0;
    // Use voltage and static resistor value to 
    // calculate FSR resistance:
    float fsrR = R_DIV * (VCC / fsrV - 1.0);
    Serial.println("Resistance: " + String(fsrR) + " ohms");
    // Guesstimate force based on slopes in figure 3 of
    // FSR datasheet:
    float force;
    force = getForce(fsrR);
    
    float fsrG = 1.0 / fsrR; // Calculate conductance
    // Break parabolic curve down into two linear slopes:
    if (fsrR <= 600) 
      force = (fsrG - 0.00075) / 0.00000032639;
    else
      force =  fsrG / 0.000000642857;
    Serial.println("Force: " + String(force) + " g");
    Serial.println();
    *someString = String(force)[0];
    
     Serial.write(someString);

    delay(500);
  }
  else
  {
    // No pressure detected
  }
}

float getForce(float fsrR) {
  float force;
  float fsrG = 1.0 / fsrR; // Calculate conductance
    // Break parabolic curve down into two linear slopes:
    if (fsrR <= 600) 
      force = (fsrG - 0.00075) / 0.00000032639;
    else
      force =  fsrG / 0.000000642857;

    return force;
}
//
//void loop() {
//
//    // wating for 10ms.  
//    delay(20);
//    serialPrintf("someInt:%d,someFloat:%f,someString:%s", someInt, someFloat, someString);
//
//}
//
//// Printing Values to the Serial port.
//// serialPrintf("someInt:%d,someFloat:%f,someString:%s", someInt, someFloat, someString);
//int serialPrintf(char *str, ...) {
//    int i, j, count = 0;
//
//    va_list argv;
//    va_start(argv, str);
//    for(i = 0, j = 0; str[i] != '\0'; i++) {
//        if (str[i] == '%') {
//            count++;
//
//            Serial.write(reinterpret_cast<const uint8_t*>(str+j), i-j);
//
//            switch (str[++i]) {
//                case 'd': Serial.print(va_arg(argv, int));
//                    break;
//                case 'l': Serial.print(va_arg(argv, long));
//                    break;
//                case 'f': Serial.print(va_arg(argv, double));
//                    break;
//                case 'c': Serial.print((char) va_arg(argv, int));
//                    break;
//                case 's': Serial.print(va_arg(argv, char *));
//                    break;
//                case '%': Serial.print("%");
//                    break;
//                default:;
//            };
//
//            j = i+1;
//        }
//    };
//    va_end(argv);
//
//    if(i > j) {
//        Serial.write(reinterpret_cast<const uint8_t*>(str+j), i-j);
//    }
//
//    Serial.println("");
//    return count;
//}

