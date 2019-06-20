using COD.Platform.Configuration.Core;
using COD.Platform.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace COD.Platform.Configuration.Basic
{
    public class PlaceholderResolver
    {

        internal const string START = "${";
        internal const string END = "}";

        // public static bool Strict = false;


        Dictionary<string, string> resolvedItems = new Dictionary<string, string>();
        private IConfiguration config;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public PlaceholderResolver(IConfiguration config)
        {
            this.config = config;
            
        }

        public string ResolveItem(string itemName, string fallbackValue = null)
        {

            //We may have resolved this before so short circuit. This is done as atomic operation to prevent race condition on config reload which will trigger resolver flush
            if (resolvedItems.TryGetValue(itemName, out var answer)) return answer;

            
            var value = config.GetString(itemName);

            var sb = new StringBuilder(value);

            
            while (ContainsPlaceholder(sb))
            {

                //Get First Completed PlaceHolder
                var firstEnd = sb.IndexOf(END);
                var matchingStart = sb.LastIndexOf(START, firstEnd);
                
                //TODO: Finish this implementation
                var newKey = sb.Replace()

            




            }

            return sb.ToString();
                       
        }

        private static string ResolveItem(string itemName, IConfiguration config,Dictionary<string,string> resolvedItems)
        {

            return string.Empty;

        }




        public static bool ContainsPlaceholder(StringBuilder key)
        {
            var i = key.IndexOf(START);
            if (i > -1)
            {
                return key.IndexOf(END, i) > -1;
            }
            return false;

        }
        public static bool ContainsPlaceholder(String key)
        {
            var i = key.IndexOf(START);
            if (i > -1)
            {
                return key.IndexOf(END, i) > -1;
            }
            return false;

        }

    }
}
