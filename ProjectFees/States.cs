using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectFees
{
    public class States
    {
        public List<StateFees> ServiceAreaStateFees = new List<StateFees>();
        public decimal OutOfAreaFee { get; private set; }

        public States()
        {
            ServiceAreaStateFees.Add(new StateFees("Arizona", "AZ", 2.99m));
            ServiceAreaStateFees.Add(new StateFees("California", "CA", 16.99m));

            OutOfAreaFee = 49.99m;
        }

        public decimal GetFeeForState(string twoLetterCode)
        {
            var state = ServiceAreaStateFees.FirstOrDefault(f => f.TwoLetterCode.Equals(twoLetterCode.ToUpper()));
            return state != null ? state.Fee : OutOfAreaFee;
        }
    }
}