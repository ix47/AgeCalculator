using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgeCalc.Models.Date_Calculations
{
    //This part of the partial class is used for seperating the constant code. 
    //This helps make the code in the other partial file look cleaner.
    public partial class HashDate
    {
        private const string JSON_RESULT_FIELD_YEAR = "Year";
        private const string JSON_RESULT_FIELD_MONTH = "Month";
        private const string JSON_RESULT_FIELD_DAY = "Day";
        private const string JSON_RESULT_FIELD_HOUR = "Hour";
        private const string JSON_RESULT_FIELD_MINUTE = "Minute";
        private const string JSON_RESULT_FIELD_SECOND = "Second";

        private const int DAYS_IN_LEAP_YEAR_FEBRUARY = 29;

        public enum Months
        {
            January = 1,
            February,
            March,
            April,
            May,
            June,
            July,
            August,
            September,
            October,
            November,
            December
        }

        public enum DateUnit
        {
            Year,
            Month,
            Day,
            Hour,
            Minute,
            Second
        }

        private Dictionary<int, int> daysInMonths = new Dictionary<int, int> {
            {(int)Months.January, 31},
            {(int)Months.February, 28},
            {(int)Months.March, 31},
            {(int)Months.April, 30},
            {(int)Months.May, 31},
            {(int)Months.June, 30},
            {(int)Months.July, 31},
            {(int)Months.August, 31},
            {(int)Months.September, 30},
            {(int)Months.October, 31},
            {(int)Months.November, 30},
            {(int)Months.December, 31}
        };

        private Dictionary<int, int> dateUnitLimits = new Dictionary<int, int> {
            {(int)DateUnit.Month, 12},
            {(int)DateUnit.Hour, 24},
            {(int)DateUnit.Minute, 60},
            {(int)DateUnit.Second, 60}
        };

    }
}