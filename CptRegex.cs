using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CptClientShared
{
    public static class CptRegex
    {
        public static Regex Name = new(@"^[a-zA-Z]+$");
        public static Regex Password = new(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,15})");
    }
}
