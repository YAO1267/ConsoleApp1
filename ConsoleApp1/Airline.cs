using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10268880K_PRG2Assignment
{
    class Airline
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Dictionary<string,Flight> Flights { get; set; }

        public Airline()
        {
            
        }

        public Airline(string n, string c, Dictionary<string,Flight> d)
        {
            Name = n;
            Code = c;
            Flights = d;
        }

        public bool AddFlight(Flight flight)
        {
            Console.Write("Enter Flight Number:");
            string newFlightNo = Console.ReadLine();
           if (flight.FlightNumber != newFlightNo)
            {
                return true;
            }
            return false;
        }

        //public double CalculateFees()
        //{
        //    return 0.00;
        //}

        public bool RemoveFlight(Flight flight)
        {
            Console.Write("Enter Flight Number:");
            string delFlightNo = Console.ReadLine();
            if (flight.FlightNumber == delFlightNo)
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return "Name:" + Name + "\tCode" + Code + "\tFlights:" + Flights;
        }
    }
}
