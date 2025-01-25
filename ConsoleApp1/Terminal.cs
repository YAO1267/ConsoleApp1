//==========================================================
// Student Number : S10268880K
// Student Name : Yao Yao
// Partner Name : Atifah 
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10268880K_PRG2Assignment
{
    class Terminal
    {
        public string TerminalName { get; set; }
        public Dictionary<string,Airline> Airlines { get; set; }
        public Dictionary<string,Flight> Flights { get; set; }
        public Dictionary<string,BoardingGate> BoardingGates { get; set; }
        public Dictionary<string,double> GateFees { get; set; }

        public Terminal()
        {
            
        }
        public Terminal(string tName, Dictionary<string, Airline> airL, Dictionary<string, Flight> f, Dictionary<string, BoardingGate> bGate, Dictionary<string, double> gFees)
        {
            TerminalName = tName;
            Airlines = airL;
            Flights = f;
            BoardingGates = bGate;
            GateFees = gFees;
        }

        public bool AddAirline(Airline airline)
        {
            foreach (KeyValuePair<string, Airline> kvp in Airlines)
            {
                if (kvp.Key == airline.Code || kvp.Value.Name == airline.Name) 
                {
                    Console.WriteLine("Invalid Airline Name/Code. This Airline already exists. Please try again.");
                    return false;
                }
            }
            
            string name = airline.Name;
            string code = airline.Code;

            Airlines.Add(name, airline);
            return true;
        }

        public bool AddBoardingGate(BoardingGate boardingGate)
        {
            if(boardingGate.Flight == null)
            {
                return true;
            }
            return false;
        }

        public Airline? GetAirlineFromFlight(Flight flight)
        {
            string codeNum = flight.FlightNumber;
            string[] split = codeNum.Split(' ');
            Airline airline;
            
            foreach (KeyValuePair<string, Airline> kvp in Airlines)
            {
                if (split[0] == kvp.Key)
                {
                    airline = kvp.Value;
                    return airline;
                }
            }

            return null;
        }

        public void PrintAirlineFees()
        {
            foreach (KeyValuePair<string, Airline> kvp in Airlines)
            {
                Console.WriteLine($"{kvp.Key} has to pay {kvp.Value.CalculateFees()}");
            }              
        }

        public override string ToString()
        {
            return "Terminal name:" + TerminalName + "\tAirlines" + Airlines + "\tFlights" + Flights + "\tBoarding Gates:" + BoardingGates + "\tGate Fees" + GateFees;
        }
    }   
}
