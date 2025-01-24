using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10268880K_PRG2Assignment
{
    class CFFTFlight :  Flight
    {
        public double RequestFee { get; set; }

        public CFFTFlight(): base() { }

        public CFFTFlight(string fNo, string o, string d, DateTime e, string s, double reFee) : base(fNo, o, d, e, s)
        {
            RequestFee = reFee;
        }

        public override double CalculateFees()
        {
            double fees = base.CalculateFees() + RequestFee;
            return fees;
        }

    }
}
