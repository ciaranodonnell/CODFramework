using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace COD.Platform.Messaging
{
    public static class TopicGenerator
    {



        public static string GenerateTopicForObject(string topicTemplate, object message)
        {
            if (topicTemplate == null) throw new ArgumentNullException("topicTemplate");
            if (message == null) throw new ArgumentNullException("message");


            StringBuilder sb = new StringBuilder();
            sb.Append(topicTemplate);

            string completeString = topicTemplate;

            if (completeString.IndexOf('{') > -1)
            {
                var newBuild = new StringBuilder();

                bool isInOne = false, malformed = false;
                var settingName = new StringBuilder();
                foreach (var c in completeString)
                {
                    if (c == '{')
                    {
                        if (isInOne)
                        {
                            malformed = true;
                            break;
                        }
                        else
                        {
                            isInOne = true;
                        }
                    }
                    else if (c == '}')
                    {
                        if (isInOne)
                        {

                            var setting = GetPropertyFromObject(message, settingName.ToString());
                            if ((setting != null))
                            {
                                newBuild.Append(setting.ToString());
                            }
                            else
                            {
                                throw new ArgumentException(
                                    string.Format("The template string contained a property name that couldn't be found ('{0}')",
                                                  settingName));
                            }
                            //Finished this one, reset to handle another
                            isInOne = false;
                            settingName.Clear();

                        }
                        else
                        {
                            malformed = true;
                            break;
                        }
                    }
                    else if (isInOne)
                    {
                        settingName.Append(c);
                    }
                    else
                    {
                        newBuild.Append(c);
                    }

                }

                if (!malformed)
                {
                    completeString = newBuild.ToString();
                }
                else
                {
                    throw new ArgumentException(
                        string.Format(
                            "The template string is malformed, it doesn't have matching {{ and }} characters. (template was '{0}')",
                            topicTemplate), "topicTemplate");
                }

            }
            return completeString;
        }


        internal static object GetPropertyFromObject(object message, string propertyName, bool throwIfMissingProperty = false)
        {
            System.Reflection.PropertyInfo prop = null;

            object targetObject = message;

            var propNames = propertyName.Split('.');


            foreach (var propName in propNames)
            {
                prop = targetObject.GetType().GetProperties().FirstOrDefault(pi => string.CompareOrdinal(pi.Name, propName) == 0);

                if (prop == null)
                {
                    if (throwIfMissingProperty)
                        throw new FormatException($"Property '{propertyName}' is not available of message of type {message.GetType().Name}");
                    else
                        return null;
                }

                targetObject = prop.GetValue(targetObject);

            }


            return targetObject;
        }
    }
}
