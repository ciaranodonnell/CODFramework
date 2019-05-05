using COD.Platform.Configuration.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace COD.Platform.Configuration.Basic
{
    public class PlaceholderResolver
    {

        public static bool Strict = false;


        public static string ResolveItem(string itemName, IConfiguration config, string fallbackValue = null)
        {

            var value = config.GetString(itemName);
            
//            value = GetNewNameFromPlaceholder(value);

            return config.GetString(value);
        }

        

    }
}
