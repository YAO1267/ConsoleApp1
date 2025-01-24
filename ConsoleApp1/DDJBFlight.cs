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
    class DDJBFlight : Flight
    {
        public double RequestFee { get; set; }
        public DDJBFlight() : base() { }

        public DDJBFlight(string fNo, string o, string d, DateTime e, string s, double reFee) : base(fNo, o, d, e, s)
        {
            RequestFee = reFee;
        }

        public double CalculateDees()
        {
            double fees = base.CalculateFees() + 300;
            return fees;
        }

        public override string ToString()
        {
            return base.ToString() + "\trequestFee:" + RequestFee;
        }

    }
}
