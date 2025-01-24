using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10268880K_PRG2Assignment
{
    class BoardingGate
    {
        public string GateName { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight Flight { get; set; }

        public BoardingGate()
        {
                  
        }

        public BoardingGate(string gName, bool sCFFT, bool sDDJB, bool sLWTT, Flight f)
        {
            GateName = gName;
            SupportsCFFT = sCFFT;
            SupportsDDJB = sDDJB;
            SupportsLWTT = sLWTT;
            Flight = f;
        }

        //public double CalculateFees()
        //{
    
        //}
        public override string ToString()
        {
            return "Gate Name:" + GateName + "\tSupports CFFT" + SupportsCFFT + "\tSupports DDJB:" + SupportsDDJB + "\tSupports LWTT" + SupportsLWTT + "\tFlight" + Flight;
        }
    }
}
