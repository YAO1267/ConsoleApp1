//==========================================================
// Student Number : S10268880K
// Student Name : Yao Yao
// Partner Name : Atifah 
// features: 23569 + advanced feature 1/a
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public Airline(string n, string c,Dictionary<string,Flight> d)
        {
            Name = n;
            Code = c;
            Flights = d;
        }

        public bool AddFlight(Flight flight)
        {
           foreach(KeyValuePair<string,Flight> kvp in Flights)
           {
                if(kvp.Key == flight.FlightNumber)
                {
                    Console.WriteLine("Invalid Flight Number. No more than 1 Flight with the same Flight Number on the same day.");
                    return false;
                }
           }

           try
           {
                string flightNo = flight.FlightNumber;
                string origin = flight.Origin;
                string destination = flight.Destination;
                string expectedTime = Convert.ToString(flight.ExpectedTime);
                string? specialCode = null;
                if(flight is NORMFlight)
                {
                    specialCode = null;
                }
                else if(flight is LWTTFlight)
                {
                    specialCode = "LWTT";
                }
                else if(flight is DDJBFlight)
                {
                    specialCode = "DDJB";
                }
                else if(flight is CFFTFlight)
                {
                    specialCode = "CFFT";
                }
                string data = flightNo + "," + origin + "," + destination + "," + expectedTime + "," + specialCode;


                try
                {
                    string projectDir = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
                    string filePath = Path.Combine(projectDir, "flights.csv");
                    using (StreamWriter sw = new StreamWriter(filePath, true))
                    {
                        sw.WriteLine(data);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }

           }
           catch (Exception e) 
           {
                Console.WriteLine($"Error: {e.Message}");
                return false;
           }
            
            Flights.Add(flight.FlightNumber, flight);
            
            return true;
        }

        public double CalculateFees()
        {
            double initialFees = 0 ;
            double specialOrigin = 0;  // to be deducted 
            double specialTime = 0;    // to be deducted
            double specialRe = 0;      // to be deducted
            double everyThree = 0;     // to ne deducted
            int numFlights = Flights.Count;
            foreach (KeyValuePair<string, Flight> kvp in Flights)
            {
                initialFees += kvp.Value.CalculateFees();
                
                if( kvp.Value.Origin == "Dubai (DXB)" || kvp.Value.Origin == "Bangkok (BKK)" || kvp.Value.Origin == "Tokyo (NRT)")
                {
                    specialOrigin += 25;
                }

                if( kvp.Value.ExpectedTime.TimeOfDay < new TimeSpan(11, 0, 0) || kvp.Value.ExpectedTime.TimeOfDay > new TimeSpan(21, 0, 0))
                {
                    specialTime += 110;
                }

                if( kvp.Value is NORMFlight)
                {
                    specialRe += 50;
                }
            }

            if (numFlights > 5)
            {
                initialFees *= 0.97;
            }

            if(numFlights >= 3)
            {
                everyThree += 350 * (Math.Floor( numFlights / 3.00));
            }

            double finalFees = initialFees - specialOrigin - specialRe - everyThree - specialTime;
            return finalFees;


        }

        public bool RemoveFlight(Flight flight)
        {
            foreach (KeyValuePair<string, Flight> kvp in Flights)
            {
                if (kvp.Key == flight.FlightNumber)
                {
                    return true;
                }
            }
            Console.WriteLine("Cannot find the Flight Number. Please try again.");
            return false;
        }

        public override string ToString()
        {
            return "Name:" + Name + "\tCode" + Code + "\tFlights:" + Flights;
        }
    }
}
