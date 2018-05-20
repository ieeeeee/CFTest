using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Basis.Utilities
{
    public class BaseIdGenerator
    {
        private readonly IdGenerator _numberGenerator = new IdGenerator(5);

        public BaseIdGenerator()
        { }

        public string GetId()
        {
            return _numberGenerator.GetId(DateTime.Now);
        }
        public string GetNo()
        {
            return _numberGenerator.GetNo();
        }
        public static readonly BaseIdGenerator Instance = new BaseIdGenerator();
    }
}
