### Description ###

Demo project using ASP.NET MVC, MQTT Broker (GnatMQ) and ESP8266 with Arduino IDE

The full solution is composed of a ASPNET MVC application integrated with a MQTT Broker that starts with the MVC.

MQTT Broker port is 1883.

Applicaiton uses Entity Framework 6.3 for database access.

### Install ###

Visual Studio 2015 Community [https://www.visualstudio.com](https://www.visualstudio.com)

Arduino IDE [http://arduino.cc/](http://arduino.cc/)

#### Arduino Libraries Used ####

ESP8266Wifi - [https://github.com/ekstrand/ESP8266wifi](https://github.com/ekstrand/ESP8266wifi)

PubSubClient - [https://github.com/knolleary/pubsubclient](https://github.com/knolleary/pubsubclient)

#### Create database ####

Inside Visual Studio, in Package Manager Console (View -> Other Windows -> Package Manager Console):

Update-Database -Force

This will create a SQL Server Localdb database and create the tables.