using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Utilities
{
    public static class Calculation
    {
        public static int CalculateTheNumberOfDays(DateTime dayOne, DateTime dayTwo)
        {
            return (dayOne - dayTwo).Days;
        }
    }
}
