# Messaging Configuration
There is a standard object that most messsage broker implementations 
need which is HMS.Platform.Messaging.ConnectionDetails

This object is able to instatiate itself from the IConfiguration interface.
It uses the following configuration:

| Key | Type | Description |
|:----|:-----|:------------|
|MESSAGING_HOST | String | The fully qualified network address of the message broker to connect to |
|MESSAGING_VPN | String | The VPN, or partition name, for the broker to use |
|MESSAGING_USER | String | The username to connect to the broker
|MESSAGING_PASS | String | The password for connecting to the broker |
|MESSAGING_IGNOREDISCARDS | Boolean | (Optional) Whether the broker should notify the client that messages have been dropped |
|MESSAGING_SENDTRYCOUNT | Integer | (Optional) How many times should the client attempt to send messages before giving up and reporting failure |
|MESSAGING_ERRORONSENDFAIL | Boolean |(Optional, Default is True) Should the API throw and error when sending fails |
|MESSAGING_DEFAULTRETRYCOUNT | Integer | (Optional) Number of times operations should be retried if they fail |