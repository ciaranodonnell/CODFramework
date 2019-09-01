using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COD.Platform.Configuration.Core
{
    public interface IConfiguration
    {
        string GetString(string key, string defaultValue= null);
        bool GetBool(string key, bool defaultValue=false);
        int GetInt32(string key, int defaultValue=0);
        string GetStringOrError(string key);
    }
}
