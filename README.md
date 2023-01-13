# A guide to using this code:

short code snippets that use the various sharedo apis.
sensitive values have been added to a configuration file that has been added to .gitignore. In order for this code to work the user will have to creat their own c# configuration file that MUST contain a public class called "Sensitive_data" that holds the sensitive data invoked in this code.

#### on authentication
In this file, the user can store the sensitive data called in the program, such as the client_id (CID), client secret (Cs), identity server endpoint to authenticate into sharedo (ID) and the api base URL (API)

* Parameters.cs calls the sensitive information contained in the Sensitive_data class in the config file. It was taken directly from sharedo's documentation, so it is best not to modify it unless you are really sure about what you are doing.

* Authenticate.cs contains the code to retrieve the oauth token that will allow one to authenticate into sharedo and then use the private apis. It is currently set to "impersonate user". If you wish to switch it back to a simple client credentials token, change the body in request header from "{ "grant_type", "Impersonate.Fixed" }" to "{ "grant_type", "client_credentials" }". Refer to the oauth documentation for extra resources on access tokens: https://oauth.net/2/ and https://help.sharedo.co.uk/en_US/calling-sharedo-apis/calling-sharedo-apis for documentation on the sharedo apis.
It is also the main entrypoint of c# file, where all the methods that call the apis are written and called in the main function.


### on other api endpoints once a user is authenticated

Please be aware that the following apis are availbe only after a user has authenticated.
You must pass the authentication token into the request headers every time you wish to call any of the below apis:

* UserInfo.cs contains a class used to deserialize json from an api call that returns user information, such as who the token you have got currently belongs to.
It calls this api endpoint "{API_BaSE_URL}/api/security/userInfo"

* WorkId.cs contains a class called WorkId, which is responsible for deserializing json returned from calling an api that accepts a MatterReference number as an input parameter and returns a multitude of information, although the most useful bit is the work item id that can then be used to call other public documented apis.
Note that this api was not documented and was reverse engineered. To a expose it, log into sharedo, inspect element -> network tab -> xhr 
start typing a matter reference number into the sharedo search-bar and look at the xhr responses.
Look for the url that has this format: "{API_BASE_URL}/api/searches/quick/legal-cases/?&q={YOUR MATTER REFERENCE NUMBER}&_={CURRENT UNIX EPOCH IN MILLISECONDS}"

* CategoryId.cs contains a class called CategoryId which is responsible for deserializing json returned from calling an api that accepts a workId string and returns information such as the categorytId (id) and a sharedo systame name (sharedoTypeSystemName). In authenticate.cs
There are two methods in authenticate.cs that use this class: the first is GetCategoryId that calls teh api and returns an integer which is the work item's category Id.
The second method is called SharedoSystem name, and returns a workid's system name.
This is the api endpoint: "{API_BASE_URL}/api/sharedo/{workid}/base?_={UnixEpoch}"
The categoryid, workid and sharedo system name are some of the parameters needed to call another api that fills in a form for a particular matter reference