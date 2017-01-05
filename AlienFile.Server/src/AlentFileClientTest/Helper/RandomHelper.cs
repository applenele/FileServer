using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlentFileClientTest.Helper
{
    public class RandomHelper
    {

        /// <summary>
        /// 获取1-n之间的随机数
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int GetRandom(int n)
        {
            if (n == 1)
            {
                return 1;
            }
            if (n <= 0)
            {
                return 0;
            }
            Random r = new Random();
            int num = r.Next(1, n + 1);
            return num;
        }
    }
}
