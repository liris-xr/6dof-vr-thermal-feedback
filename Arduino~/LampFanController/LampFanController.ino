#include <DMXSerial.h>

void setup() {
  DMXSerial.init(DMXController);
  Serial.begin(115200);

  while (!Serial) {
    ;
  }
  Serial.print("DMX\n");  //For handshake
}
// the loop function runs over and over again forever
void loop() {

  String serialReceived;
  if (Serial.available() > 0) {
    serialReceived = Serial.readStringUntil('\n');
    Serial.println(serialReceived);

    char *argv[8];
    int argc;
    split(argv, &argc, serialReceived.c_str(), ';', 0);
    int channel = atoi(argv[0]);
    int value = atoi(argv[1]);
    if (channel == 0) {
      for (int i = 1; i <= 512; i++) DMXSerial.write(i, value);
    } else {
      DMXSerial.write(channel, value);
    }
  }
}

char **split(char **argv, int *argc, char *string, const char delimiter, int allowempty) {
  *argc = 0;
  do {
    if (*string && (*string != delimiter || allowempty)) {
      argv[(*argc)++] = string;
    }
    while (*string && *string != delimiter) string++;
    if (*string) *string++ = 0;
    if (!allowempty)
      while (*string && *string == delimiter) string++;
  } while (*string);
  return argv;
}
