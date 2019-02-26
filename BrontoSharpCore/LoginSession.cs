using Bronto.API.BrontoService;
using System.Threading.Tasks;
using sessionHeader = Bronto.API.BrontoService.sessionHeader;


namespace Bronto.API
{
    /// <summary>
    /// Wrapper class for the bronto login session. All API Wrapper classes are initialized with this class
    /// </summary>
    public class LoginSession
    {
        /// <summary>
        ///  Returns the current login session Id
        /// </summary>
        public string SessionId { get; private set; }
        
        /// <summary>
        /// Creates a new Login session
        /// </summary>
        /// <param name="ApiToken">A valid API token. See dev.bronto.com for details</param>
        /// <returns>The Login session</returns>
        public static async Task<LoginSession> CreateAsync(string ApiToken)
        {
            BrontoSoapPortTypeClient client = BrontoSoapClient.Create();
            LoginSession login = new LoginSession();
            loginResponse response = await client.loginAsync(ApiToken);
            login.SessionId = response.@return;
            client = null;
            return login;
            
        }

        /// <summary>
        /// Returns the sessionHeader used in all API calls to the Bronto Web service
        /// </summary>
        internal sessionHeader SessionHeader
        {
            get {
                return new sessionHeader() { sessionId = this.SessionId };
            }
        }


    }
}
