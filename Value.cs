using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgCmp
{
    class Value
    {
        static string val1, val2;
        public static string rtbstr1
        {
            get
            {
                return val1;
            }
            set
            {
                val1 = value;
            }
        }
        public static string rtbstr2
        {
            get
            {
                return val2;

            }
            set
            {
                val2 = value;
            }
        }
    }
}
