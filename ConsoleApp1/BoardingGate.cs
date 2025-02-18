﻿//==========================================================
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

        public double CalculateFees()
        {
            if (SupportsCFFT)
            {
                return 150;
            }
            else if (SupportsDDJB)
            {
                return 300;
            }
            else if(SupportsLWTT)
            {
                return 500;
            }
            
            return 300;
        }
        public override string ToString()
        {
            return "Gate Name:" + GateName + "\tSupports CFFT" + SupportsCFFT + "\tSupports DDJB:" + SupportsDDJB + "\tSupports LWTT" + SupportsLWTT + "\tFlight" + Flight;
        }
    }
}
