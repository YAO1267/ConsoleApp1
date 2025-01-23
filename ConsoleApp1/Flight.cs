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
		public string flightNumber { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public DateTime expectedTime { get; set; }
        public string status { get; set; }

        public Flight(string fNo, string o, string d, DateTime e, string s)
        {
            flightNumber = fNo;
            origin = o;
            destination = d;
            expectedTime = e;
            status = s;
        }

        public double CalculateFees()
        {
            
        }

        public override string ToString()
        {
            return "flightnumber:" + flightNumber + "\torigin:" + origin + "\tdestination" + destination + "\texpectedtime:" + expectedTime + "\tstatus:" + status;
        }
    }
}
