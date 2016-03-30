using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Util.Store;
using Google.GData.Client;
using Google.GData.Spreadsheets;

namespace GetOAuthTokens
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var stream = new FileStream("client_secrets_linus.json", FileMode.Open, FileAccess.Read))
            {
                var clientSecrets = new ClientSecrets
                {
                    ClientId = "316950573639-mmqt2s1k26hck8ieg81q22kgooq3u76f.apps.googleusercontent.com",
                    ClientSecret = "aKiSwFBQG3pwD59p7V26RBVN"
                };
                var scopes = new[] { "https://spreadsheets.google.com/feeds", "https://docs.google.com/feeds" };
                var flow = new GoogleAuthorizationCodeFlow(
                    new GoogleAuthorizationCodeFlow.Initializer
                    {
                        ClientSecrets = clientSecrets,
                        Scopes = scopes,
                    });


                var cred = GoogleWebAuthorizationBroker.AuthorizeAsync(clientSecrets, scopes, "user", CancellationToken.None).Result;


                //GoogleWebAuthorizationBroker.ReauthorizeAsync(cred, CancellationToken.None).Wait();

                //var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                //    GoogleClientSecrets.Load(stream).Secrets,
                //    new[] { "https://spreadsheets.google.com/feeds", "https://docs.google.com/feeds" },
                //    "user", CancellationToken.None).Result;
                //var accessToken = credential.GetAccessTokenForRequestAsync().Result;

                Console.ReadLine();
            }
        }
    }
}
