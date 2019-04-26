using COD.Platform.Messaging.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COD.Platform.Messaging.Test.TestOfTheMocks
{
 internal static   class TestingHelpers
    {

        internal static MockMessagingService GetMessaingService()
        {
            return new MockMessagingService();

        }

    }
}
