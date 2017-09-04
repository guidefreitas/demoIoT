### Description ###

Demo project using ASP.NET MVC and ESP8266 with Arduino IDE

Applicaiton uses Entity Framework 6.3 for database access.

### Install ###

Visual Studio 2015 Community [https://www.visualstudio.com](https://www.visualstudio.com)

Arduino IDE [http://arduino.cc/](http://arduino.cc/)

#### Arduino Libraries Used ####

ESP8266Wifi - [https://github.com/ekstrand/ESP8266wifi](https://github.com/ekstrand/ESP8266wifi)

#### Create database ####

Inside Visual Studio, in Package Manager Console (View -> Other Windows -> Package Manager Console):

Update-Database -Force

This will create a SQL Server Localdb database and create the tables.