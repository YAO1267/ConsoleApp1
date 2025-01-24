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
    class LWTTFlight : Flight
    {
        public double RequestFee { get; set; }
        public LWTTFlight() : base() { }


        public LWTTFlight(string fNo, string o, string d, DateTime e, string s, double reFee) : base(fNo, o, d, e, s)
        {
            RequestFee = reFee;
        }

        public double CalculateFees()
        {
            double fees = base.CalculateFees() + RequestFee;
            return fees;
        }

        public override string ToString()
        {
            return base.ToString() + "\trequestFee:" + RequestFee;
        }

    }
}
