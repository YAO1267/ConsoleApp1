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

namespace S10268880K_PRG2Assignment
{
    class NORMFlight : Flight
    {
        public NORMFlight() : base() { }


        public NORMFlight(string fNo, string o, string d, DateTime e, string s) : base(fNo, o, d, e, s)
        { }

        public override double CalculateFees()
        {
            return base.CalculateFees();
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }
    
}
