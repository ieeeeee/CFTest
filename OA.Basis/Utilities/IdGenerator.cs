using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Basis.Utilities
{
    public class IdGenerator
    {
        private readonly long _max;
        private int _seed;
        private readonly object _locker = new object();
        private int _no;
        private readonly object _noLocker = new object();
  
        public IdGenerator(int seedWith)
        {
            _max = (long)Math.Pow(10, seedWith) - 1;
        }

        private const string TimeFormat = "yyMMdd";

        public string GetId(DateTime time)
        {
            var prefix = time.ToString(TimeFormat);
            //var stamp = (time.Hour * 3600 + time.Minute * 60 + time.Second).ToString().PadLeft(5, '0');
            var hour = time.Hour.ToString().PadLeft(2, '0');
            var min = time.Minute.ToString().PadLeft(2, '0');
            lock(_locker) //lock 确保当一个线程位于代码的临界区时，另一个线程不进入临界区。如果其他线程试图进入锁定的代码，则它将一直等待（即被阻止），直到该对象被释放。
            {
                _seed++;
                var id = string.Format("{0}{1}{2}{3}", prefix, hour,min, _seed.ToString().PadLeft(4, '0'));
                if(_seed>=_max)
                {
                    _seed = 0;
                }
                return id;
            }
        }
        public string GetNo()
        {
            var prefix = "0";
            lock (_noLocker) //lock 确保当一个线程位于代码的临界区时，另一个线程不进入临界区。如果其他线程试图进入锁定的代码，则它将一直等待（即被阻止），直到该对象被释放。
            {
                _no++;
                var no = string.Format("{0}{1}", prefix,  _seed.ToString().PadLeft(2, '0'));
                if (_no >= 99)
                {
                    _no = 0;
                }
                return no;
            }
        }
    }
}
