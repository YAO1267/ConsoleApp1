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
            Console.Write("Enter Flight Number:");
            string newAirline = Console.ReadLine();
            if (airline.Name != newAirline)
            {
                return true;
            }
            return false;
        }       
        //public bool AddBoardingGate(BoardingGate boardingGate)
        //{
        //    if()
        //}
    }   
}
