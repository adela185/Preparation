using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectFees
{
    public class StateFees
{
    public string Name { get; private set; }
    public string TwoLetterCode { get; private set; }
    public decimal Fee { get; private set; }
    public StateFees(string name, string twolettercode, decimal fee)
    {
        Name = name;
        TwoLetterCode = twolettercode;
        Fee = fee;
    }
}
}
