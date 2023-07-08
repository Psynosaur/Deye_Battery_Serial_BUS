## Deye_Battery_CAN_BUS

CAN BUS tree of known frame types for use with [CAN Monitor 3000](https://github.com/tixiv/CAN-Monitor-qt) on Windows as a standalone application

Based on this device:

![image](https://github.com/Psynosaur/Deye_Battery_CAN_BUS/assets/26934113/04c1c34b-6d6d-4141-acb8-f41646d75c32)

Unix implementation can be done with [usb-can](https://github.com/kobolt/usb-can).

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


## Current Web API CAN data output
```JSON
{
    "Frame_0355": {
        "data": "100, 100, 0, 0"
    },
    "Frame_0305": {
        "data": "0, 0, 0, 0"
    },
    "Frame_0200": {
        "data": "3438, 3383, 190, 180"
    },
    "Frame_0359": {
        "data": "0, 0, 0, 0"
    },
    "Frame_0371": {
        "data": "0, 1800, 0, 0"
    },
    "Frame_0251": {
        "data": "170, -400, 0, 100"
    },
    "Frame_0361": {
        "data": "3438, 3374, 190, 170"
    },
    "Frame_0364": {
        "data": "514, 0, 2, 0"
    },
    "Frame_0151": {
        "data": "543, 0, 1000, 1000"
    },
    "Frame_0250": {
        "data": "190, -400, 0, 100"
    },
    "Frame_0401": {
        "data": "1, 3, 528, 18"
    },
    "Frame_0550": {
        "data": "18933, 0, 15151, 0"
    },
    "Frame_0150": {
        "data": "546, 0, 1000, 1000"
    },
    "Frame_0750": {
        "data": "0, 0, 0, 0"
    },
    "Frame_0501": {
        "data": "1056, 22186, 11831, 13872"
    },
    "Frame_035E": {
        "data": "22852, 12336, 817, 2000"
    },
    "Frame_0351": {
        "data": "576, 0, 1800, 480"
    },
    "Frame_035C": {
        "data": "0, 0, 0, 0"
    },
    "Frame_0363": {
        "data": "1056, 544, 0, 0"
    },
    "Frame_0601": {
        "data": "12336, 12341, 12337, 12336"
    },
    "Frame_0400": {
        "data": "1, 3, 5184, 18"
    },
    "Frame_0551": {
        "data": "18876, 0, 15149, 0"
    },
    "Frame_0600": {
        "data": "12336, 12341, 12337, 12336"
    },
    "Frame_0201": {
        "data": "3410, 3374, 180, 170"
    },
    "Frame_0751": {
        "data": "0, 0, 0, 0"
    },
    "Frame_0356": {
        "data": "5440, 0, 170, 0"
    },
    "Frame_0111": {
        "data": "0, 0, 0, 8448"
    },
    "Frame_0110": {
        "data": "0, 0, 0, 8448"
    },
    "Frame_0500": {
        "data": "1056, 22186, 11831, 13872"
    },
    "Frame_0701": {
        "data": "0, 0, 0, 0"
    },
    "Frame_0700": {
        "data": "1, 0, 0, 0"
    }
}
```
	

