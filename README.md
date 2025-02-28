# 6dof-vr-thermal-feedback
<p align="center">
  Repository for the IEEE 2025 Poster - <b>Dynamic and Modular Thermal Feedback for Interactive 6DoF VR: Implementation and Evaluation</b>
</p>
<p align="center">
  Sophie Villenave, Pierre Raimbaud, Guillaume Lavoué
</p>
<!-- <p align="center">
  <a href="google.com">[PDF]</a> <a href="google.com">[Poster]</a>
</p> -->

## Overview
Creating ambient thermal feedback for VR experience is a challenge, and while numerous solutions in the litterature exist to provide thermal sensation, they are often very complex and under-documented. As this makes replication tedious, we propose a simple and tried system, that uses infrared lamps and fans, which can be easily controlled and are proven to provide quality thermal sensation while in VR, which enhances is shown to enhance user quality of experience, and can also be crucial to specific applications. This repository hosts the Unity package we developed to control our thermal feedback system from a VR application. It also contains the technical documentation required to replicate the system as it is presented in the poster.

## Thermal Feedback System
### Hardware Shopping List
* 4 x 300W Infrared Lamps (Link)
* 4 x Lamp Holders (Link)
* 1 x DMX 4-Way Dimmer (Link)
* 4 x DMX Fans (Link)
* 1 x Arduino Mega (Link)
* 1 x DMX Shield for Arduino (Link)
* 2 x Jumper Wire Female to Female (Link)
* 5 x 3-pin DMX Cables (Link)

### Arduino DMX Controller
#### Requirements  
- **Arduino Mega**. Other arduino models won't work because the DMX shield uses the RX and TX ports of the board.
- **DMX Shield**
- **USB-B Cable**  
- **Arduino IDE** (download from [Arduino's official site](https://www.arduino.cc/en/software))  

#### Flashing Firmware
1. **Connect your Arduino**  
   - Plug in the Arduino via USB.  
   - Open **Arduino IDE**.  

2. **Select the Correct Board & Port**  
   - Use the dropdown to select the connected Arduino Mega.  

3. **Install Required Libraries**  
   - Open `Sketch` → `Include Library` → `Manage Libraries...`  
   - Install [DMXSerial](https://github.com/mathertel/DMXSerial)

4. **Upload the Firmware**  
   - Open the provided firmware `.ino` file. It is available in the Samples folder of the Unity Package.  
   - Click `Verify` (✔) to compile the code.  
   - Click `Upload` (→) to flash the firmware onto the Arduino.  

5. **Confirm Installation**  
   - Open `Tools` → `Serial Monitor`.  
   - Set the baud rate to **115200** (or as specified).  
   - If you see "DMX" in the monitor, the firmware is successfully installed.  

### Wiring
TBD

## Unity Package
### Prerequisites  
This package requires Unity to be configured with the **.NET Framework** because it relies on **System.IO.Ports** for Serial Port communication with the DMX controller.  

**Enable .NET Framework in your Unity Project**
1. Open `Edit` → `Project Settings` → `Player`.  
2. Under the `Other Settings` section, find `API Compatibility Level`.  
3. Set it to `.NET Framework`. 
4. Restart Unity for the changes to take effect.

### Installing via Unity Package Manager (UPM)  
1. Open your Unity project.  
2. Navigate to `Window` → `Package Manager`.  
3. Click on the `+` button (top-left) and select `Add package from git URL...`.  
4. Enter the repository Git URL:  

   ```
   https://github.com/liris-xr/6dof-vr-thermal-feedback.git
   ```  

5. Click `Add` and wait for Unity to download and install the package.  
6. Once installed, import the package's samples in your project.

## Usage  
TBD 

## Troubleshooting  
### Common Issues  
1. **System.IO.Ports Namespace Missing**  
   - Ensure **.NET Framework** is enabled in **Player Settings**. 


## License  
GPL v3  

## Citation
TBD
