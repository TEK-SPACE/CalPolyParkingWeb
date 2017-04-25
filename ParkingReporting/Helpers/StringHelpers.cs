﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingReporting.Helpers
{
    public static class StringHelpers
    {
        public static string addParams(this string bbase, IDictionary<string,string> parameters)
        {
            var result = bbase + "?";
            foreach (string key in parameters.Keys)
            {
                result += key + "=" + parameters[key] + "&";
            }
            result = result.Substring(startIndex: 0, length: result.Length - 1);
            return result;
        }
    }
}
