using CptClientShared.QueryForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared
{
    public class CptLibReport
    {
        public string Name { get; set; } = string.Empty;
        public List<string> Properties { get; set; } = new();
    }
}
