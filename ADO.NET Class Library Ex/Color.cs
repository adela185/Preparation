using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_Class_Library_Ex
{
    public class Color
    {
        [Required(ErrorMessage = "Name is required"),
            StringLength(50, ErrorMessage = "Name too long; keep under 50 characters")]
        public string nm { get; set; }

        [Required(ErrorMessage = "Hex is required"),
            StringLength(6, MinimumLength = 6, ErrorMessage = "Must be 6 characters")]
        public string hex { get; set; }

        //[RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+*")] an attribute for valid emails
    }
}
