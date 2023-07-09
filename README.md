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
    "ChargeVoltage": 0,
    "CurrentLimit": 0,
    "DischargeLimit": 0,
    "BatteryCutoffVoltage": 0,
    "ChargeCurrentLimit": 0,
    "ChargeCurrentLimitMax": 0,
    "CurrentStateVoltage": 0,
    "CurrentStateCurrent": 0,
    "CurrentStateTemperature": 0,
    "CellVoltageHigh": 0,
    "CellVoltageLow": 0,
    "BmsTemperatureHigh": 0,
    "BmsTemperatureLow": 0,
    "BatteryCapacity": 0,
    "FullChargedRestingVoltage": 0,
    "Statuses": {
        "BatteryCount": 0,
        "Status1": 0,
        "Status2": 0,
        "Status3": 0,
        "Status4": 0,
        "Status5": 0,
        "Status6": 0,
        "Status7": 0
    },
    "Batteries": [],
    "CanFrames": [
        {
            "FrameId": "0251",
            "Data1": 120,
            "Data2": -400,
            "Data3": 0,
            "Data4": 100
        },
        {
            "FrameId": "0111",
            "Data1": 0,
            "Data2": 0,
            "Data3": 0,
            "Data4": 8448
        },
        {
            "FrameId": "0361",
            "Data1": 3338,
            "Data2": 3332,
            "Data3": 120,
            "Data4": 120
        },
        {
            "FrameId": "0355",
            "Data1": 100,
            "Data2": 100,
            "Data3": 0,
            "Data4": 0
        },
        {
            "FrameId": "0200",
            "Data1": 3338,
            "Data2": 3332,
            "Data3": 120,
            "Data4": 120
        },
        {
            "FrameId": "0351",
            "Data1": 576,
            "Data2": 0,
            "Data3": 1800,
            "Data4": 480
        },
        {
            "FrameId": "0359",
            "Data1": 0,
            "Data2": 0,
            "Data3": 0,
            "Data4": 0
        },
        {
            "FrameId": "0250",
            "Data1": 120,
            "Data2": -400,
            "Data3": 0,
            "Data4": 100
        },
        {
            "FrameId": "0110",
            "Data1": 0,
            "Data2": 0,
            "Data3": 0,
            "Data4": 8448
        },
        {
            "FrameId": "0364",
            "Data1": 514,
            "Data2": 0,
            "Data3": 2,
            "Data4": 0
        },
        {
            "FrameId": "0371",
            "Data1": 0,
            "Data2": 1800,
            "Data3": 0,
            "Data4": 0
        },
        {
            "FrameId": "035C",
            "Data1": 0,
            "Data2": 0,
            "Data3": 0,
            "Data4": 0
        },
        {
            "FrameId": "035E",
            "Data1": 22852,
            "Data2": 12336,
            "Data3": 817,
            "Data4": 2000
        },
        {
            "FrameId": "0500",
            "Data1": 1056,
            "Data2": 22186,
            "Data3": 11831,
            "Data4": 13872
        },
        {
            "FrameId": "0401",
            "Data1": 1,
            "Data2": 3,
            "Data3": 0,
            "Data4": 18
        },
        {
            "FrameId": "0363",
            "Data1": 1056,
            "Data2": 544,
            "Data3": 0,
            "Data4": 0
        },
        {
            "FrameId": "0201",
            "Data1": 3338,
            "Data2": 3332,
            "Data3": 120,
            "Data4": 120
        },
        {
            "FrameId": "0151",
            "Data1": 533,
            "Data2": 0,
            "Data3": 1000,
            "Data4": 1000
        },
        {
            "FrameId": "0356",
            "Data1": 5330,
            "Data2": 0,
            "Data3": 120,
            "Data4": 0
        },
        {
            "FrameId": "0305",
            "Data1": 0,
            "Data2": 0,
            "Data3": 0,
            "Data4": 0
        },
        {
            "FrameId": "0400",
            "Data1": 1,
            "Data2": 3,
            "Data3": 0,
            "Data4": 18
        },
        {
            "FrameId": "0651",
            "Data1": 13122,
            "Data2": 13105,
            "Data3": 12594,
            "Data4": 13366
        },
        {
            "FrameId": "0150",
            "Data1": 533,
            "Data2": 0,
            "Data3": 1000,
            "Data4": 1000
        },
        {
            "FrameId": "0550",
            "Data1": 18933,
            "Data2": 0,
            "Data3": 15151,
            "Data4": 0
        },
        {
            "FrameId": "0601",
            "Data1": 12336,
            "Data2": 12341,
            "Data3": 12337,
            "Data4": 12336
        },
        {
            "FrameId": "0700",
            "Data1": 1,
            "Data2": 0,
            "Data3": 0,
            "Data4": 0
        },
        {
            "FrameId": "0551",
            "Data1": 18876,
            "Data2": 0,
            "Data3": 15149,
            "Data4": 0
        },
        {
            "FrameId": "0750",
            "Data1": 0,
            "Data2": 0,
            "Data3": 0,
            "Data4": 0
        },
        {
            "FrameId": "0650",
            "Data1": 13122,
            "Data2": 13105,
            "Data3": 12594,
            "Data4": 13111
        },
        {
            "FrameId": "0600",
            "Data1": 12336,
            "Data2": 12341,
            "Data3": 12337,
            "Data4": 12336
        },
        {
            "FrameId": "0501",
            "Data1": 1056,
            "Data2": 22186,
            "Data3": 11831,
            "Data4": 13872
        },
        {
            "FrameId": "0701",
            "Data1": 0,
            "Data2": 0,
            "Data3": 0,
            "Data4": 0
        },
        {
            "FrameId": "0751",
            "Data1": 0,
            "Data2": 0,
            "Data3": 0,
            "Data4": 0
        }
    ]
}
```
	

