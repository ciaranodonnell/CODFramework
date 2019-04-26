using System;
using System.Collections.Generic;
using System.Text;

namespace COD.Platform.DataAccess
{
    public interface IDatabaseConnectionInfo
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        string Username { get; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        string Password { get; }

        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>
        /// The server.
        /// </value>
        string Server { get; }

        /// <summary>
        /// Gets or sets the server port.
        /// </summary>
        /// <value>
        /// The server.
        /// </value>
        int? Port { get; }

        /// <summary>
        /// Gets or sets the name of the DB.
        /// </summary>
        /// <value>
        /// The name of the DB.
        /// </value>
        string DBName { get; }

        /// <summary>
        /// Gets or sets the DSN.
        /// Usually used for connection to C++ libs
        /// </summary>
        /// <value>
        /// DSN
        /// </value>
        string DSN { get; }


        /// <summary>
        /// Gets the full name of the application. This is usefull when looking in SQL Server logs and traces
        /// </summary>
        /// <value>
        /// The full name of the application.
        /// </value>
        string FullApplicationName { get; }
    }
}
