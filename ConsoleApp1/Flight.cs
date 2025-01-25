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
    abstract class Flight
    {
		public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; }

        protected Flight()
        {
            
        }
        public Flight(string fNo, string o, string d, DateTime e, string s)
        {
            FlightNumber = fNo;
            Origin = o;
            Destination = d;
            ExpectedTime = e;
            Status = s;
        }

        public virtual double CalculateFees()
        {
            double fees = 300;
            if (Destination == "Singapore (SIN)" )
            {
                fees += 500;
                return fees;
            }
            else 
            {
                fees += 800;
                return fees;
            }
        }

        public override string ToString()
        {
            return "flightnumber:" + FlightNumber + "\torigin:" + Origin + "\tdestination" + Destination + "\texpectedtime:" + ExpectedTime + "\tstatus:" + Status;
        }
    }
}
