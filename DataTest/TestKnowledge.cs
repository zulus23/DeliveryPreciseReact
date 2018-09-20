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
            list.Add("\"СК\"");
            list.Add("\"СП\"");
            list.Add("\"ПР\"");
            

            string join = string.Join(",", list.ToArray());
            Assert.Contains("СП",join);
            list.Clear();
            string join1 = string.Join(",", list.ToArray());

            string p = string.Format("===== {0} ----- {1} ========= {0} --------------- {1}", 23, 78);
            System.Console.WriteLine(string.Format("===== {0} ----- {1} ========= {0} --------------- {1}", 23, 78));

        }
        
        
    }
}