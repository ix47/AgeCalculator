using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgeCalc.Models.Date_Calculations
{
    public partial class HashDate
    {
        private int[] units = new int[6];

        public int Day { get { return units[(int)DateUnit.Day]; } }
        public int Month { get { return units[(int)DateUnit.Month]; } }
        public int Year { get { return units[(int)DateUnit.Year]; } }

        public int Hour { get { return units[(int)DateUnit.Hour]; } }
        public int Minute { get { return units[(int)DateUnit.Minute]; } }
        public int Second { get { return units[(int)DateUnit.Second]; } }

        private HashDate() : this(0, 0, 0, 0, 0, 0) { }

        public HashDate(HashDate copyFrom) : this(copyFrom.Year, copyFrom.Month, copyFrom.Day, copyFrom.Hour, copyFrom.Minute, copyFrom.Second) { }

        public HashDate(int year, int month, int day) : this(year, month, day, 0, 0, 0) { }

        public HashDate(DateTime dateTime) : this(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second) { }

        public HashDate(int year, int month, int day, int hour, int minute, int second)
        {
            units[(int)DateUnit.Year] = year;
            units[(int)DateUnit.Month] = month;
            units[(int)DateUnit.Day] = day;
            units[(int)DateUnit.Hour] = hour;
            units[(int)DateUnit.Minute] = minute;
            units[(int)DateUnit.Second] = second;
        }

        public int getNumberOfDaysInMonth() { return getNumberOfDaysInMonth(this.Year, this.Month); }

        public int getNumberOfDaysInMonth(int year, int month)
        {
            if (month == 2)
            {
                //Check if it is a leap year.
                if (isLeapYear())
                {
                    return DAYS_IN_LEAP_YEAR_FEBRUARY;
                }
            }
            return daysInMonths[month];
        }

        public bool isLeapYear() { return isLeapYear(this.Year); }

        public bool isLeapYear(int year)
        {
            if (year % 4 == 0)
            {
                return true;
            }
            return false;
        }

        public int getPreviousMonth(int currentMonth)
        {
            return ((currentMonth - 1) < 1) ? 12 : (currentMonth - 1);
        }

        public HashDate Difference(HashDate withDate)
        {
            HashDate diff = new HashDate(this);

            //NOTE: It is very important to calculate the difference in ascending order below.
            //Reversing this causing serious issues in the calculations.

            //Get the difference in seconds.
            diff.decreaseUnit(DateUnit.Second, withDate.Second);

            //Get the difference in minutes.
            diff.decreaseUnit(DateUnit.Minute, withDate.Minute);

            //Get the difference in hours.
            diff.decreaseUnit(DateUnit.Hour, withDate.Hour);

            //Get the difference in days.
            diff.decreaseUnit(DateUnit.Day, withDate.Day);

            //Get the difference in months.
            diff.decreaseUnit(DateUnit.Month, withDate.Month);

            //Get the difference in year.
            diff.decreaseUnit(DateUnit.Year, withDate.Year);

            return diff;
        }

        public void increaseUnit(DateUnit unit, int value)
        {
            int unitLimit;

            switch (unit)
            {
                case DateUnit.Year:
                    unitLimit = Int32.MaxValue;
                    break;

                case DateUnit.Day:
                    unitLimit = getNumberOfDaysInMonth();
                    break;

                default:
                    unitLimit = dateUnitLimits[(int)unit];
                    break;
            }

            int existingValue = units[(int)unit];
            int newValue = existingValue + value;

            if (newValue > unitLimit)
            {
                units[(int)unit] = newValue - unitLimit;

                //Now increase the previous unit's value.
                int previousUnit = ((int)unit) - 1;
                increaseUnit((DateUnit)previousUnit, 1);
            }
            else
            {
                units[(int)unit] = newValue;
            }

        }

        public void decreaseUnit(DateUnit unit, int value)
        {
            int unitLimit;

            switch (unit)
            {
                case DateUnit.Year:
                    unitLimit = Int32.MaxValue;
                    break;

                case DateUnit.Day:
                    int previousMonth = getPreviousMonth(this.Month);
                    int useYear = previousMonth == 12 ? this.Year - 1 : this.Year;
                    unitLimit = getNumberOfDaysInMonth(useYear, previousMonth);
                    break;

                default:
                    unitLimit = dateUnitLimits[(int)unit];
                    break;
            }

            int existingValue = units[(int)unit];
            int newValue = existingValue - value;

            if (newValue < 0)
            {
                //Since newValue is already a negative value, we will add it instead of subtracting it.
                //Otherwise, subtracting a negative value will end up adding a positive value since, -(-1) = 1.
                units[(int)unit] = unitLimit + newValue;

                //Now decrease the previous unit's value.
                int previousUnit = ((int)unit) - 1;
                decreaseUnit((DateUnit)previousUnit, 1);
            }
            else
            {
                units[(int)unit] = newValue;
            }

        }

        public Dictionary<string, int> getAsDictionary()
        {
            Dictionary<string, int> hashDateDictionary = new Dictionary<string, int>();
            hashDateDictionary.Add(JSON_RESULT_FIELD_YEAR, this.Year);
            hashDateDictionary.Add(JSON_RESULT_FIELD_MONTH, this.Month);
            hashDateDictionary.Add(JSON_RESULT_FIELD_DAY, this.Day);
            hashDateDictionary.Add(JSON_RESULT_FIELD_HOUR, this.Hour);
            hashDateDictionary.Add(JSON_RESULT_FIELD_MINUTE, this.Minute);
            hashDateDictionary.Add(JSON_RESULT_FIELD_SECOND, this.Second);
            return hashDateDictionary;
        }
    }
}