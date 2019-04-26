using COD.Platform.Configuration.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace COD.Platform.Messaging
{
    /// <summary>
    /// Basic Connection information that is needed by all message brokers
    /// </summary>
    public class ConnectionDetails
    {
        public static readonly string ConfigKey_MessagingPassword = "MESSAGING_PASS";
        public static readonly string ConfigKey_MessagingUser = "MESSAGING_USER";
        public static readonly string ConfigKey_MessagingVpn = "MESSAGING_VPN";
        public static readonly string ConfigKey_MessagingHost = "MESSAGING_HOST";
        public static readonly string ConfigKey_MessagingIgnoreDiscards = "MESSAGING_IGNOREDISCARDS";
        public static readonly string ConfigKey_SendTryCount = "MESSAGING_SENDTRYCOUNT";
        public static readonly string ConfigKey_ThrowOnSendFailure = "MESSAGING_ERRORONSENDFAIL";
        public static readonly string ConfigKey_ConnectionTryCount = "MESSAGING_DEFAULTRETRYCOUNT";
        public static readonly string ConfigKey_ClientId = "MESSAGING_CLIENTID";

        /// <summary>
        /// This version of the constructor is for edge cases where standard configuration is not used. 
        /// </summary>
        [Obsolete("This version of the constructor is for edge cases where standard configuration is not used. ")]
        public ConnectionDetails()
        {

        }

        /// <summary>
        /// The Server, Username, and Password values are the minimum configuration values required
        /// </summary>
        /// <param name="configuration"></param>
        public ConnectionDetails(IConfiguration configuration)
        {
            Password = configuration.GetStringOrError(ConfigKey_MessagingPassword);
            Username = configuration.GetStringOrError(ConfigKey_MessagingUser);
            Vpn = configuration.GetString(ConfigKey_MessagingVpn);
            Server = configuration.GetStringOrError(ConfigKey_MessagingHost);
            IgnoreDiscards = configuration.GetBool(ConfigKey_MessagingIgnoreDiscards, false);
            ThrowOnFailure = configuration.GetBool(ConfigKey_ThrowOnSendFailure, false);
            SendTryCount = configuration.GetInt32(ConfigKey_SendTryCount, 1);
            DefaultReconnectRetries = configuration.GetInt32(ConfigKey_ConnectionTryCount, 1);
            ClientId = configuration.GetString(ConfigKey_ClientId);
        }
        public string Vpn { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Server { get; set; }

        public bool IgnoreDiscards { get; set; }
        public int SendTryCount { get; set; }
        public bool ThrowOnFailure { get; set; }
        public int DefaultReconnectRetries { get; set; }
        public double AutoReconnectTime { get; set; }
        public string ClientId { get; set; }
    }
}
