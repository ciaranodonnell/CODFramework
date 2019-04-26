using System;
using System.Collections.Generic;
using System.Text;

namespace COD.Platform.DataAccess
{
   
	public class DatabaseConnectionInfo : IDatabaseConnectionInfo
    {

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }
        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>
        /// The server.
        /// </value>
        public string Server { get; set; }

        /// <summary>
        /// Gets or sets the server port.
        /// </summary>
        /// <value>
        /// The server.
        /// </value>
        public int? Port { get; set; }

        /// <summary>
        /// Gets or sets the name of the DB.
        /// </summary>
        /// <value>
        /// The name of the DB.
        /// </value>
        public string DBName { get; set; }

        /// <summary>
        /// Gets or sets the DSN.
        /// Usually used for connection to C++ libs
        /// </summary>
        /// <value>
        /// DSN
        /// </value>
        public string DSN { get; set; }



        public string FullApplicationName { get; set; }
    }
}
