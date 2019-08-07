using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string egn = "8207116355";
            string year = "1982";

            var a = year.Substring(2,2);
            var b = egn.Substring(0, 2);
        }
    }
}

