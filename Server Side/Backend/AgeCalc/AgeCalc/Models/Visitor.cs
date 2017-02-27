using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgeCalc.Models
{
    public class Visitor
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Dictionary<string, int> Age { get; set; }
    }
}