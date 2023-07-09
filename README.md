## Deye_Battery_CAN_BUS Reader

CAN BUS tree of known frame types for use with [CAN Monitor 3000](https://github.com/tixiv/CAN-Monitor-qt) on Windows as a standalone application

Based on this device:

![image](https://github.com/Psynosaur/Deye_Battery_CAN_BUS/assets/26934113/04c1c34b-6d6d-4141-acb8-f41646d75c32)

Unix implementation can be done with [usb-can](https://github.com/kobolt/usb-can).

### Hardware connection side 
![image](https://github.com/Psynosaur/Deye_Battery_CAN_BUS/assets/26934113/e02b6207-d4ea-4d4d-a50a-e3e462aa1385)

![image](https://github.com/Psynosaur/Deye_Battery_CAN_BUS/assets/26934113/86d262d1-b2b0-4e49-8af9-236f0f778b98)

### CAN Monitor 3000 Windows application

![image](https://github.com/Psynosaur/Deye_Battery_CAN_BUS/assets/26934113/fcef5139-bc05-49d1-b6c4-5e910b7498f6)


### Current Web API CAN data output
```JSON
{
  "SOC": 100,
  "SOH": 100,
  "Voltage": 53.2,
  "CurrentStateCurrent": 0,
  "CurrentStateTemperature": 0,
  "CellVoltageHigh": 3.332,
  "CellVoltageLow": 3.328,
  "BmsTemperatureHigh": 13,
  "BmsTemperatureLow": 12,
  "ChargeCurrentLimit": 0,
  "ChargeCurrentLimitMax": 180,
  "CutoffVoltage": 48,
  "ChargeVoltage": 57.6,
  "CurrentLimit": 0,
  "DischargeLimit": 180,
  "BatteryCapacity": 211.2,
  "RestingVoltage": 54.4,
  "Batteries": [
    {
      "Voltage": 53.2,
      "Current": 0,
      "StateOfCharge": 100,
      "StateOfHealth": 100,
      "CellHigh": 3.332,
      "CellLow": 3.328,
      "CellDelta": 0.004,
      "Temp1": 13,
      "Temp2": 13,
      "TemperatureMos": 14,
      "CurrentLimit": 0,
      "CurrentLimitMax": 100,
      "ChargedTotal": 18.933,
      "DischargedTotal": 15.151
    },
    {
      "Voltage": 53.2,
      "Current": 0,
      "StateOfCharge": 100,
      "StateOfHealth": 100,
      "CellHigh": 3.332,
      "CellLow": 3.329,
      "CellDelta": 0.003,
      "Temp1": 13,
      "Temp2": 12,
      "TemperatureMos": 14,
      "CurrentLimit": 0,
      "CurrentLimitMax": 100,
      "ChargedTotal": 18.876,
      "DischargedTotal": 15.149
    }
  ],
  "Statuses": {
    "Status1": 2,
    "Status2": 2,
    "Status3": 0,
    "Status4": 0,
    "Status5": 2,
    "Status6": 0,
    "Status7": 0,
    "Status8": 0
  }
}
```
	

