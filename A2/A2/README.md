# CSE 445/598 E-Ticketing
Jared Ham

I completed this project as an individual. 

## Environment & Setup
- This project was created using Visual Studio 2019
- Import the project to Visual Studio by opening the CSE598-A2-E-Ticketing.sln file 
- The main class is located in MainClass.cs

## Decisions and Assumptions Made
All decisions and assumptions made about the project are listed below
### Order Processing
- Valid credit card numbers are in the range 5000 <= # <= 7000
- Total amount to charge is calculated as unitPrice * numTickets + Tax + LocationCharge
- Where Tax = 0.05 * unitPrice*numTickets 
- Location Charge = 3  

### Park 
- The park randomly generates a new price between 80 and 300 anytime a price change occurs
- The park updates its pricing at a random interval between 200ms and 500ms


### Ticket Agency
- There are a total of 5 ticketing agencies
- A ticketing agency will order tickets 20% of the time  when a price cut does not occur. In this case the number of tickets ordered will be a random number between 10 and 20
- When a price drop occurs ticketing agencies will purchase tickets 70% of the time, a ticketing agency will purchase between 50 and 400 tickets 


