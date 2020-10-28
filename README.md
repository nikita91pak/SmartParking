# Description
This is an open source project for “smart parking system”. The purpose of the project is to make cars parking a little bit more smart than a regular parking. The program can monitor all entering and outgoing cars at the parking.
The are two parts of the program:

# Client
This is a program that will be installed on the PC at the entrance to the parking. It’s written in C# language and connects to remote database (DB) to fetch all the cars from the DB. 

**Purpose:** 
 - Detecting the licence plate from the picture that has been taken at the entrance.
 - Check in the DB the permissions to enter, expiration date etc.
 - Command to the barrier to open 

# Administration tool
This tool is for controlling the DB. [DEMO](https://smartparkingadministration.herokuapp.com/)
The source code of this tool is [here](https://github.com/iedenis/smartparkingadministration)

**Purpose:** 
- Setting the necessary permissions for the drivers (allow, forbidden)
- Searching for drivers and deleting them from the DB
- Searching for the cars inside the parking or outside


# Administration client for "Smart Parking project" 

This tool is administration client for the project "Smart Parking". 
With this tool the admin can:

 - Insert new cars that have a permission
 - Delete cars if their permission has bees expired
 - Update the status of the cars 
- sdfsdnsdg