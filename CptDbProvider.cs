using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared
{
    public class CptDbProvider
    {
        private ConceptContext currentContext = new();
        public ConceptContext CurrentContext => currentContext;
        public ConceptContext NewContext()
        {
            currentContext.Dispose();
            currentContext = new ConceptContext();
            return currentContext;
        }
        public int SaveDb() => currentContext.SaveChanges();
    }
}
