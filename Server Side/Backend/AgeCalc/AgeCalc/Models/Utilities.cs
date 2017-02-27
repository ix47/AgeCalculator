using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgeCalc.Models
{
    public static class Utilities
    {
        public static object ExtractParameter(FormCollection collection, string parameterName, Type type)
        {
            var parameter = collection[parameterName];

            if (string.IsNullOrWhiteSpace(parameter))
            {
                throw new Exception(string.Format("Paramter with name '{0}' is empty.", parameterName));
            }

            if (type == typeof(bool))
            {
                return Convert.ToBoolean(Convert.ToInt32(parameter));
            }

            var result = Convert.ChangeType(parameter, type);

            return result;
        }
    }
}