using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace COD.Platform.Logging
{
 public static   class Helpers
    {

        #region Figure Out Current Class Logger
        public static string LookupHostClassNameFromStackFrame(System.Diagnostics.StackFrame stackFrame)
        {
            var method = stackFrame.GetMethod();

            if (method != null)
            {
                string str = GetStackFrameMethodClassName(method, true, true, true) ?? method.Name;
                if (!string.IsNullOrEmpty(str) && str.StartsWith("COD.", StringComparison.Ordinal))
                {
                    return str;
                }
            }
            return string.Empty;
        }
        public static string GetStackFrameMethodClassName(System.Reflection.MethodBase method, bool includeNameSpace, bool cleanAsyncMoveNext, bool cleanAnonymousDelegates)
        {
            if (method == null)
            {
                return null;
            }
            Type declaringType = method.DeclaringType;
            if (((cleanAsyncMoveNext && (method.Name == "MoveNext")) && (declaringType?.DeclaringType != null)) && (declaringType.Name.StartsWith("<") && (declaringType.Name.IndexOf('>', 1) > 1)))
            {
                declaringType = declaringType.DeclaringType;
            }
            if ((!includeNameSpace && (declaringType?.DeclaringType != null)) && (declaringType.IsNested && (declaringType.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Length > 0)))
            {
                return declaringType.DeclaringType.Name;
            }
            string str = includeNameSpace ? declaringType?.FullName : declaringType?.Name;
            if (cleanAnonymousDelegates && (str != null))
            {
                int index = str.IndexOf("+<>", StringComparison.Ordinal);
                if (index >= 0)
                {
                    str = str.Substring(0, index);
                }
            }
            return str;
        }


        #endregion

    }
}
