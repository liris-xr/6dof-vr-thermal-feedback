# 6dof-vr-thermal-feedback
<p align="center">
  Repository for the IEEE VR 2025 Poster - <b>Dynamic and Modular Thermal Feedback for Interactive 6DoF VR: Implementation and Evaluation</b>
</p>
<p align="center">
  Sophie Villenave, Pierre Raimbaud, Guillaume Lavoué
</p>
<!-- <p align="center">
  <a href="google.com">[PDF]</a> <a href="google.com">[Poster]</a>
</p> -->

## Overview
Creating ambient thermal feedback for VR experience is a challenge, and while numerous solutions in the litterature exist to provide thermal sensation, they are often very complex and under-documented. As this makes replication tedious, we propose a simple and tried system, that uses infrared lamps and fans, which can be easily controlled and is proven to provide quality thermal sensation while in VR, which is shown to enhance user quality of experience, and can also be crucial to specific applications. This repository hosts the Unity package we developed to control our thermal feedback system from a VR application. It also contains the technical documentation required to replicate the system as it is presented in the poster.

>__*New:*__ __Obstacle Awareness__ 
>
>Obstacless between the thermal source and the user will be detected and the thermal feedback will be adjusted accordingly.

## Thermal Feedback System
### Hardware Shopping List
* 4 x [300W Infrared Lamps](https://uk.rs-online.com/web/p/heat-lamps/7897909)
* 4 x Lamp Holders
* 1 x [DMX 4-Way Dimmer](https://www.thomann.co.uk/stairville_dds_405_lc_dmx_4_ch._dimmer.htm)
* 4 x [DMX Fans](https://www.thomann.co.uk/adj_entour_cyclone.htm)
* 1 x [Arduino Mega](https://store.arduino.cc/products/arduino-mega-2560-rev3?srsltid=AfmBOorwUxOtEAnLyQ9JH7IWtvPmKv4zfdIC7xoHMsMAQSeB1WqoUrns))
* 1 x USB-B 2.0 (Arduino) to USB-A or USB-C (PC) cable. Arduino Mega use **USB-B 2.0** connector, which differs from USB-B 3.0 connector.
* 1 x [DMX Shield for Arduino](https://www.digikey.co.uk/en/products/detail/dfrobot/DFR0260/7087143)
* 1 x [Shield Stack Connectors ARD85](https://www.digikey.fr/en/products/detail/adafruit-industries-llc/85/5154649)
* 2 x Jumper Wire Female to Female
* 5 x [3-pin DMX Cables](https://www.thomann.co.uk/stairville_pdc3cc_dmx_cable_50_m_3_pin.htm)

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

4. **Modify DMX Serial to use RX/TX 1**
   - DMX Serial Library files can be found in the `libraries\DMXSerial\src` folder of your Arduino IDE installation.
   - Within Arduino IDE, go to `File` → `Preferences` → `Sketchbook Location` to locate the folder.
   - To free Serial Port 0 for USB connection with the computer, you need to change the port used by the DMX shield. To do so, enable the definitions for Serial Port 1 in the DMXSerial library file ``src\DMXSerial_avr.h`` by uncommenting the following line:
```
#define DMX_USE_PORT1
```

5. **Upload the Firmware**  
   - Open the provided firmware `LampFanController.ino` file. It is available in the `Arduino~` folder of the Unity Package.  
   - Click `Verify` (✔) to compile the code.  
   - Click `Upload` (→) to flash the firmware onto the Arduino.  

6. **Confirm Installation**  
   - Open `Tools` → `Serial Monitor`.  
   - Set the baud rate to **115200** (or as specified).  
   - If you see "DMX" in the monitor, the firmware is successfully installed.  

### Wiring
Plug DMX Shield on the Arduino board using the stack connectors except for the RX/TX pins. Connect the DMX Shield RX/TX pins to the Serial Port 1 RX/TX pins on the Arduino board using 2 F/F jumper wires. 

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
### In your scene, add:
1. Arduinio DMX Controller game object *(ArduinoDMXController.cs and OnDMXOpenHandler.cs)*. Make sure to set the band rate to 115200.
2. ThemalListener.cs to the MainCamera. **[Recommended]**
3. Themal sources *(ThermalSource.cs)*. 
 >There are three types of themal sources you can choose from:
 > 
 > 1. **Envieronment:** Activate the thermal feedback uniformly no matter where the user is in the scene.
 > 2. **Fall off:** Varies the thermal feedback based on inverse square law (No spatial awareness).
 >3. **Spatial:** Like the name suggests, the thermal feedback is computed based on the scatial orientation of the user.
>
>__For Spatial source:__ In the layers dropdown, select all the layers you want the Source to detect. Make sure the user layer is included. For the player game objct, it is recommended to use a *capsule* 3D object which is attached to the XR Origin.

4. Themal Devices
5. Thermal Controller
 



   

## Troubleshooting  
### Common Issues  
1. **System.IO.Ports Namespace Missing**  
   - Ensure **.NET Framework** is enabled in **Player Settings**.
2. **Automatic retrieval of COM port fails**
   - Ensure your project scripting backend is set to **Mono** in **Player Settings**. GetSerialPortNames() is not supported via **IL2CPP**. [Unity Forum Thread](https://discussions.unity.com/t/il2cpp-system-io-serialport-arduino/736323/5)


## License  
GPL v3  

## Citation
S. Villenave, P. Raimbaud and G. Lavoué, "Dynamic and Modular Thermal Feedback for Interactive 6DoF VR: Implementation and Evaluation," 2025 IEEE Conference on Virtual Reality and 3D User Interfaces Abstracts and Workshops (VRW), Saint Malo, France, 2025, pp. 1204-1205, doi: 10.1109/VRW66409.2025.00248.

@INPROCEEDINGS{10973033,
  author={Villenave, Sophie and Raimbaud, Pierre and Lavoué, Guillaume},
  booktitle={2025 IEEE Conference on Virtual Reality and 3D User Interfaces Abstracts and Workshops (VRW)}, 
  title={Dynamic and Modular Thermal Feedback for Interactive 6DoF VR: Implementation and Evaluation}, 
  year={2025},
  volume={},
  number={},
  pages={1204-1205},
  doi={10.1109/VRW66409.2025.00248}}
