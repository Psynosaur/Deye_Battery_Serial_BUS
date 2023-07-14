### Known frame id's 

| Frame ID | Description                                                          | Format of 8 data bytes(signed bits in little endian) |
|----------|----------------------------------------------------------------------|------------------------------------------------------|
|          | **CAN Network Devices**                                              |                                                      |
| 305      | Inverter Addr/Status                                                 | s8,s8,s8,s8,s8,s8,s8,s8                              |
| 359      | Battery #1 CAN Status                                                | s8,s8,s8,s8,s8,s8,s8,s8                              |
| 35C      | Battery #2 CAN Status                                                | s8,s8,s8,s8,s8,s8,s8,s8                              |
|          | **BMS Properties**                                                   |                                                      |
| 351      | Chg(V), MaxChgCur(A), MaxDisChgCur(A), CutOff(V)                     | s16,s16,s16,s16                                      |
| 355      | SOC, SOH, *, *                                                       | s16,s16,s16,s16                                      |
| 356      | SysVolt(V), SysCurr(A), SysTemp(°C), *                               | s16,s16,s16,s16                                      |
| 35E      | Model number : DY001                                                 | HEX to ASCII                                         |
| 361      | SysMaxCellVolt(V), SysMinCellVolt(V), SysTempMax(°C), SysTempMin(°C) | s16,s16,s16,s16                                      |
| 363      | SysCapacity(Ah), FullChgVolt(V), *, *                                | s16,s16,s16,s16                                      |
| 364      | Status and SysBattCnt(5th byte in array)                             | s8,s8,s8,s8,s8,s8,s8,s8                              |
| 371      | MaxChgCur(A), MaxDisChgCur(A), *, *                                  | s16,s16,s16,s16                                      |
|          | **Battery Properties**                                               |                                                      |
|          | **+1 to Frame ID per additional battery**                            |                                                      |
| 110      | Alarms?                                                              | s8,s8,s8,s8,s8,s8,s8,s8                              |
| 150      | Voltage, Current, SOC, SOH                                           | s16,s16,s16,s16                                      |
| 200      | Cell Voltage (High, Low), Temp (1, 2)                                | s16,s16,s16,s16                                      |
| 250      | MOS Temp, ? , Limits (Current, Max)                                  | s16,s16,s16,s16                                      |
| 400      | Status: 1;2;0, cycles(2nd byte), *, 15;9;, 3-18                      | s8,s8,s8,s8,s8,s8,s8,s8                              |
| 500      | Boot version                                                         | HEX to ASCII                                         |
| 550      | Power IN and Out                                                     | s32,s32                                              |
| 600      | Serial number first half                                             | HEX to ASCII                                         |
| 650      | Serial number second half                                            | HEX to ASCII                                         |
| 700      | ID 700                                                               | s8,s8,s8,s8,s8,s8,s8,s8                              |
| 750      | ID 750                                                               | s8,s8,s8,s8,s8,s8,s8,s8                              |
