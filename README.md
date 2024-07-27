## Deye battery serial bus reader

### Data
  - CAN data frames
    - Full array information
  - RS485 individual cell voltages 
    - **Will move to seperate project soon**

### Software prerequisites 
  - InfluxDB on some remote server
    - Setup orginisation and bucket and replace values in `appsetting.json`
    
    ![influx.png](./images/influx.png)
  

### Dashboard API's
    https://xxx.xxx.xxx.xxx:5035/candata
    https://xxx.xxx.xxx.xxx:5035/rs485

### Grafana dashboard (file included) 
![image](https://github.com/Psynosaur/Deye_Battery_Serial_BUS/assets/26934113/9ae661c9-07a9-4ad3-8502-b58bbeacd145)


### Was built from findings with [CAN Monitor 3000](https://github.com/tixiv/CAN-Monitor-qt)

Includes CAN BUS tree (XML) of known frame types

#### CAN Monitor 3000 Windows application

![image](https://github.com/Psynosaur/Deye_Battery_CAN_BUS/assets/26934113/fcef5139-bc05-49d1-b6c4-5e910b7498f6)

#### Based on this device:

![image](https://github.com/Psynosaur/Deye_Battery_CAN_BUS/assets/26934113/04c1c34b-6d6d-4141-acb8-f41646d75c32)

#### Hardware connection side 
![image](https://github.com/Psynosaur/Deye_Battery_CAN_BUS/assets/26934113/905d121c-b3f2-4eac-9578-d9c0cf407022)


![image](https://github.com/Psynosaur/Deye_Battery_CAN_BUS/assets/26934113/86d262d1-b2b0-4e49-8af9-236f0f778b98)

## TODO
 - Add support for InterCAN-BUS messages
   - Cell voltages 
   - 6x Temp sensors
   - Should require only **one** CAN device for all information
 - Add setup details for raspberry pi
 - CAN frames
   - Debugging for Frame analysis
 - Split RS485 and CAN into seperate projects
 - 
 - Document reverse engineering process in more detail
  - RS485 serial payload




	

