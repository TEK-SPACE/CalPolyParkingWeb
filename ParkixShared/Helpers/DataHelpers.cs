using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;


namespace Parkix.Shared.Helpers
{
    /// <summary>
    /// Helpers for data manipulation.
    /// </summary>
    public static class DataHelpers
    {
        /// <summary>
        /// Updates the target object's properties with the non-null values of the update object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="update"></param>
        public static void Update<T>(ref T target, T update)
        {
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                if (property.GetValue(update) != null)
                {
                    property.SetValue(target, property.GetValue(update, null), null);
                }
            }
        }

    }
}