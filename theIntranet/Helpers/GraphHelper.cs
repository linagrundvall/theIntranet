//using Microsoft.Graph;
//using Microsoft.Identity.Client;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;
//using System.Net.Http.Headers;
//using System.Security.Claims;
//using System.Web;

//namespace theIntranet.Helpers
//{
//    public class GraphHelper
//    {
//        //Load configuration settings from PrivateSettings.config
//        private static string appId = System.Configuration.ConfigurationManager.AppSettings["ida:AppId"];
//        private static string appSecret = System.Configuration.ConfigurationManager.AppSettings["ida:AppSecret"];
//        private static string redirectUri = System.Configuration.ConfigurationManager.AppSettings["ida:RedirectUri"];
//        private static List<string> graphScopes =
//            new List<string>(System.Configuration.ConfigurationManager.AppSettings["ida:AppScopes"].Split(' '));

//        public static async Task<IEnumerable<Event>> GetEventsAsync()
//        {
//            var graphClient = GetAuthenticatedClient();

//            var events = await graphClient.Me.Events.Request()
//                .Select("subject,organizer,start,end")
//                .OrderBy("createdDateTime DESC")
//                .GetAsync();

//            return events.CurrentPage;
//        }

//        public static async Task<ProfilePhoto> GetProfilePhotoAsync()
//        {
//            var graphClient = GetAuthenticatedClient();
//            var photo = await graphClient.Me.Photo.Request()
//                .GetAsync();

//            return photo;
//        }

//        public static async Task<User> GetUserAsync()
//        {
//            var apiUrl = "https://graph.microsoft.com/v1.0/me";
//            var graphClient = GetAuthenticatedClient();
//            var user = await graphClient.Me.Request()
//                .GetAsync();

//            return user;
//        }

//        private static GraphServiceClient GetAuthenticatedClient()
//        {
//            return new GraphServiceClient(
//                new DelegateAuthenticationProvider(
//                    async (requestMessage) =>
//                    {
//                        var idClient = ConfidentialClientApplicationBuilder.Create(appId)
//                            .WithRedirectUri(redirectUri)
//                            .WithClientSecret(appSecret)
//                            .Build();

//                        var tokenStore = new SessionTokenStore(idClient.UserTokenCache,
//                                HttpContext.Current, ClaimsPrincipal.Current);

//                        var userUniqueId = tokenStore.GetUsersUniqueId(ClaimsPrincipal.Current);
//                        var account = await idClient.GetAccountAsync(userUniqueId);

//                        // By calling this here, the token can be refreshed
//                        // if it's expired right before the Graph call is made
//                        var result = await idClient.AcquireTokenSilent(graphScopes, account)
//                                    .ExecuteAsync();

//                        requestMessage.Headers.Authorization =
//                            new AuthenticationHeaderValue("Bearer", result.AccessToken);
//                    }));
//        }
//    }
//}