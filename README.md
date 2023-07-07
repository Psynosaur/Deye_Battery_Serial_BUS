## Deye_Battery_CAN_BUS

CAN BUS tree of known frame types for use with [CAN Monitor 3000](https://github.com/tixiv/CAN-Monitor-qt) on Windows as a standalone application

Based on this device:

![image](https://github.com/Psynosaur/Deye_Battery_CAN_BUS/assets/26934113/04c1c34b-6d6d-4141-acb8-f41646d75c32)

Unix implementation can be done with [this repo](https://github.com/kobolt/usb-can).

### Hardware connection side 
![image](https://github.com/Psynosaur/Deye_Battery_CAN_BUS/assets/26934113/e02b6207-d4ea-4d4d-a50a-e3e462aa1385)

![image](https://github.com/Psynosaur/Deye_Battery_CAN_BUS/assets/26934113/86d262d1-b2b0-4e49-8af9-236f0f778b98)

### CAN Monitor 3000 Windows application

![image](https://github.com/Psynosaur/Deye_Battery_CAN_BUS/assets/26934113/fcef5139-bc05-49d1-b6c4-5e910b7498f6)

## Format strings
You can type format strings into the field "Format String" to decode the data of the CAN message.

The format string consists of up to 8 fields seperated by a comma (",") that have the following format:

TYPE [MULTIPLIER][:[TEXT][[0].NUM_DIGITS][TEXT]]

#### Type specifiers

|specifier|description|
|--|--|
|u8 or U8|unsigned 8 bits|
|u16|unsigned 16 bits little endian|
|u32|unsigned 32 bits little endian|
|s8 or S8|signed 8 bits|
|s16|signed 16 bits little endian|
|s32|signed 32 bits little endian|
|U16|unsigned 16 bits big endian|
|U32|unsigned 32 bits big endian|
|S16|signed 16 bits big endian|
|S32|signed 32 bits big endian|
|f|floating point 32 bits little endian (IEE754 single precision)|

## TODO 
  - Make a grafana interface for this
    - Or plot the data somewhere and in a database
