using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DataTest
{
    public class TestKnowledge
    {

        [Fact]
        public void StringJoinFromListStringMustReturnString()
        {
            List<string> list = new List<string>();
            list.Add("СК");
            list.Add("СП");
            list.Add("ПР");

            string p1 = list.Aggregate(" AND IN (", (i, a) => i = i + "'" + a + "'"+",",e => e + "'' )").Replace(",''","");  
            

            string p = string.Format("===== {0} ----- {1} ========= {0} --------------- {1}", 23, 78);
            System.Console.WriteLine(string.Format("===== {0} ----- {1} ========= {0} --------------- {1}", 23, 78));

        }
        
        
    }
}