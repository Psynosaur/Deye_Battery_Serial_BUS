## Deye_Battery_CAN_BUS Reader

Grabs CAN Serial Frames in the background and serves an API endpoint for battery and BMS data 

    https://xxx.xxx.xxx.xxx:5035/Candata

| Frame ID | Description                                                                                        | Format of 8 data bytes(signed bits in little endian) |
|----------|----------------------------------------------------------------------------------------------------|------------------------------------------------------|
|          | **CAN Network Devices**                                                                            |                                                      |
| 305      | Inverter Addr/Status                                                                               | s8,s8,s8,s8,s8,s8,s8,s8                              |
| 359      | Battery #1 CAN Status                                                                              | s8,s8,s8,s8,s8,s8,s8,s8                              |
| 35C      | Battery #2 CAN Status                                                                              | s8,s8,s8,s8,s8,s8,s8,s8                              |
|          | **BMS Properties**                                                                                 |                                                      |
| 351      | Chg(V), MaxChgCur(A), MaxDisChgCur(A), CutOff(V)                                                   | s16,s16,s16,s16                                      |
| 355      | SOC, SOH, *, *                                                                                     | s16,s16,s16,s16                                      |
| 356      | SysVolt(V), SysCurr(A), SysTemp(°C), *                                                             | s16,s16,s16,s16                                      |
| 35E      | Model number : DY001                                                                               | HEX to ASCII                                         |
| 361      | SysMaxCellVolt(V), SysMinCellVolt(V), SysTempMax(°C), SysTempMin(°C)                               | s16,s16,s16,s16                                      |
| 363      | SysCapacity(Ah), FullChgVolt(V), *, *                                                              | s16,s16,s16,s16                                      |
| 364      | Status and SysBattCnt(5th byte in array)                                                           | s8,s8,s8,s8,s8,s8,s8,s8                              |
| 371      | MaxChgCur(A), MaxDisChgCur(A), *, *                                                                | s16,s16,s16,s16                                      |
|          | **Battery Properties** add 1 to each Frame ID for each additional battery(check BMS battery count) |                                                      |
| 110      | Alarms?                                                                                            | s8,s8,s8,s8,s8,s8,s8,s8                              |
| 150      | Voltage, Current, SOC, SOH                                                                         | s16,s16,s16,s16                                      |
| 200      | Cell Voltage (High, Low), Temp (1, 2)                                                              | s16,s16,s16,s16                                      |
| 250      | MOS Temp, * , Limits (Current, Max)                                                                | s16,s16,s16,s16                                      |
| 400      | Status: 1;2;0, cycles(2nd byte), *, 15;9;, 3-18                                                    | s8,s8,s8,s8,s8,s8,s8,s8                              |
| 500      | Boot version                                                                                       | HEX to ASCII                                         |
| 550      | Power IN and Out                                                                                   | s32,s32                                              |
| 600      | Serial number first half                                                                           | HEX to ASCII                                         |
| 650      | Serial number second half                                                                          | HEX to ASCII                                         |
| 700      | ID 700                                                                                             | s8,s8,s8,s8,s8,s8,s8,s8                              |
| 750      | ID 750                                                                                             | s8,s8,s8,s8,s8,s8,s8,s8                              |


## TODO 
 - Add frame descriptions - Done
   - Ideally should read CAN Monitor 3000 xml and parse data based on those formats  
 - Add setup details for raspberry pi
 - Add RS485 support for Deye batteries
   - Per cell voltage metrics
   - ![image](https://github.com/Psynosaur/Deye_Battery_CAN_BUS/assets/26934113/db687aa1-d8e1-4c50-9894-56bb3609e6ce)

   - Alarms
 - Split CAN debugging from data api
   - Debugging for Frame analysis
   - Data API only for known Frame ids
 - Should open serial device for product and vendor ids, to open on any Operating System


### Current Web API CAN data output
```JSON
{
    "SOC": 100,
    "SOH": 100,
    "Voltage": 54,
    "Amp": 0,
    "Temp": 0,
    "Cell(V) H": 3.383,
    "Cell(V) L": 3.373,
    "Cell(V) d": 0.010,
    "BmsTempHigh": 13,
    "BmsTempLow": 12,
    "ChargeLimit(A)": 0,
    "ChargeLimitMax(A)": 180,
    "Cut off(V)": 48,
    "Charge(V)": 57.6,
    "CurrentLimit(A)": 0,
    "DischargeLimit(A)": 180,
    "BatteryCapacity(Ah)": 211.2,
    "Resting(V)": 54.4,
    "Statuses": {
        "=>": "2, 0, 0, 0, 2, 0, 0, 0"
    },
    "Batteries": [
        {
            "Voltage": 54,
            "Current": 0,
            "StateOfCharge": 100,
            "StateOfHealth": 100,
            "CellHigh": 3.381,
            "CellLow": 3.373,
            "CellDelta": 0.008,
            "Temp1": 13,
            "Temp2": 13,
            "TemperatureMos": 14,
            "CurrentLimit": 0,
            "CurrentLimitMax": 100,
            "ChargedTotal": 18.933,
            "DischargedTotal": 15.151,
            "Cycles": 2.9591796875,
            "Status": {
                "=>": "0, 0, 3, 0, 0, 0, 3, 0"
            }
        },
        {
            "Voltage": 54,
            "Current": 0,
            "StateOfCharge": 100,
            "StateOfHealth": 100,
            "CellHigh": 3.383,
            "CellLow": 3.373,
            "CellDelta": 0.010,
            "Temp1": 13,
            "Temp2": 12,
            "TemperatureMos": 14,
            "CurrentLimit": 0,
            "CurrentLimitMax": 100,
            "ChargedTotal": 18.876,
            "DischargedTotal": 15.149,
            "Cycles": 2.9587890625,
            "Status": {
                "=>": "0, 0, 3, 0, 0, 0, 3, 0"
            }
        }
    ],
    "CanFrames": [
        {
            "FrameId": "0371",
            "Hex": "AA 55 01 01 01 71 03 00 00 08 00 00 08 07 00 00 00 00 00 8E",
            "DataSize": 8,
            "Data1": 0,
            "Data2": 1800,
            "Data3": 0,
            "Data4": 0,
            "LastUpdated": "2023-07-09 21:08:34"
        },
        ...
    ]
}
```

### Was built from findings with [CAN Monitor 3000](https://github.com/tixiv/CAN-Monitor-qt)

Includes CAN BUS tree (XML) of known frame types

#### CAN Monitor 3000 Windows application

![image](https://github.com/Psynosaur/Deye_Battery_CAN_BUS/assets/26934113/fcef5139-bc05-49d1-b6c4-5e910b7498f6)

#### Based on this device:

![image](https://github.com/Psynosaur/Deye_Battery_CAN_BUS/assets/26934113/04c1c34b-6d6d-4141-acb8-f41646d75c32)

#### Hardware connection side 
![image](https://github.com/Psynosaur/Deye_Battery_CAN_BUS/assets/26934113/e02b6207-d4ea-4d4d-a50a-e3e462aa1385)

![image](https://github.com/Psynosaur/Deye_Battery_CAN_BUS/assets/26934113/86d262d1-b2b0-4e49-8af9-236f0f778b98)





	

