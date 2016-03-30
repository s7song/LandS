using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.GData.Client;
using Google.GData.Spreadsheets;

namespace MassEmail
{

    public class InviteDetails
    {
        public string Url { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
        public bool MultiplePeople { get; set; }
        public bool? IsComing { get; set; }
        public string Remarks { get; set; }
        public bool InvitedToReception { get; set; }
        public string Emails { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var finishedEmails = new List<string>();
            try
            {
                var service = GetSpreadSheetService();
                var listFeed = GetListFeed(service);

                var i = 0;
                foreach (var entry in listFeed.Entries.Cast<ListEntry>().Select(ToInviteDetails)
                    .Where(x => x.InvitedToReception && !String.IsNullOrWhiteSpace(x.Emails) && x.IsComing.HasValue && x.IsComing.Value))
                {
                    i++;
                    SendEmail(entry.Name, entry.Url, entry.Emails.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries));

                    if (i % 100 == 0)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(60));
                    }

                    Console.WriteLine("Sent to {0}", entry.Name);
                    finishedEmails.AddRange(entry.Emails.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries));
                }
            }
            catch
            {
                File.WriteAllText("c:\\finishedEmails.txt", String.Join(",", finishedEmails));
            }
            //SendEmail("Agnes + Sunny", "testing", new[] { "agnesandsunny@gmail.com" });
            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        private static void SendEmail(string name, string url, string[] toAddress)
        {
            var client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("agnesandsunny@gmail.com", "vscouwixynoqjoim");

            var msg = new MailMessage()
            {
                From = new MailAddress(
                    address: "agnesandsunny@gmail.com",
                    displayName: "Agnes and Sunny"
                ),
                Body = GetEmailBody(name, "http://www.agnesandsunny.com/" + url),
                Subject = "Oops, it's totally 2 days past our wedding instead!",
                IsBodyHtml = true,
            };

            foreach (var address in toAddress)
                msg.To.Add(address);
            client.Send(msg);
        }

        private static string GetEmailBody(string name, string fullUrl)
        {
            return String.Format(@"
<p>Refer to our previous email, LOL</p>

<p>Cheers!<br/>
- Agnes and Sunny</p>", name);
        }

        private static InviteDetails ToInviteDetails(ListEntry entry)
        {
            var inviteDetails = new InviteDetails();
            foreach (ListEntry.Custom column in entry.Elements)
            {
                if (String.Equals(column.LocalName, "url", StringComparison.OrdinalIgnoreCase))
                    inviteDetails.Url = column.Value;
                else if (String.Equals(column.LocalName, "message", StringComparison.OrdinalIgnoreCase))
                    inviteDetails.Message = column.Value;
                else if (String.Equals(column.LocalName, "name", StringComparison.OrdinalIgnoreCase))
                    inviteDetails.Name = column.Value;
                else if (String.Equals(column.LocalName, "multiplepeopleyesno", StringComparison.OrdinalIgnoreCase))
                    inviteDetails.MultiplePeople = String.Equals(column.Value, "yes", StringComparison.OrdinalIgnoreCase);
                else if (String.Equals(column.LocalName, "comingyesno", StringComparison.OrdinalIgnoreCase))
                    inviteDetails.IsComing = String.IsNullOrWhiteSpace(column.Value) ? null : (bool?)String.Equals(column.Value, "yes", StringComparison.OrdinalIgnoreCase);
                else if (String.Equals(column.LocalName, "remarks", StringComparison.OrdinalIgnoreCase))
                    inviteDetails.Remarks = column.Value;
                else if (String.Equals(column.LocalName, "emails", StringComparison.OrdinalIgnoreCase))
                    inviteDetails.Emails = column.Value;
                else if (String.Equals(column.LocalName, "invitetoreception", StringComparison.OrdinalIgnoreCase))
                    inviteDetails.InvitedToReception = !String.IsNullOrWhiteSpace(column.Value) && String.Equals(column.Value, "yes", StringComparison.OrdinalIgnoreCase);
            }

            return inviteDetails;
        }

        private static SpreadsheetsService GetSpreadSheetService()
        {
            var clientSecrets = new ClientSecrets { ClientId = "981859081773-3g5el2cunikchk57monen3jg6m62tl3e.apps.googleusercontent.com", ClientSecret = "n6bhu8_eIpFAEWJqa7uglpNz" };
            var scopes = new[] { "https://spreadsheets.google.com/feeds", "https://docs.google.com/feeds" };
            var flow = new GoogleAuthorizationCodeFlow(
                new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = clientSecrets,
                    Scopes = scopes,
                });

            var cred = new UserCredential(flow, "user", new TokenResponse
            {
                AccessToken = "ya29.4ADw6Gpw_rplQGvnfsQfZPixisp5gzrhdmYdOX4t2cQCxgOp-XAotrh8rjUPBlKISdRHvSXXS-_skw",
                RefreshToken = "1/gYz1ZTmWKftvau4X0NC8rglhjJ2NQIMs-cDpr687MGYMEudVrK5jSpoR30zcRFq6"
            });

            var oauth = new OAuth2Parameters
            {
                ClientId = clientSecrets.ClientId,
                ClientSecret = clientSecrets.ClientSecret,
                Scope = String.Join(" ", scopes),
                AccessToken = cred.Token.AccessToken,
                RefreshToken = cred.Token.RefreshToken,
            };

            var applicationName = "agnes-sunny-wedding";
            var requestFactory = new GOAuth2RequestFactory(null, applicationName, oauth);
            return new SpreadsheetsService(applicationName) { RequestFactory = requestFactory };
        }

        private static ListFeed GetListFeed(SpreadsheetsService service)
        {
            var spreadsheet = service.Get("https://spreadsheets.google.com/feeds/spreadsheets/private/full/1vtEBb6kKnHoDB_0ICSxzrF-BXWBQvOAJZN5EPXrlYO8") as SpreadsheetEntry;
            var worksheet = spreadsheet.Worksheets.Entries.FirstOrDefault(x => x.Title.Text.Equals("Website", StringComparison.OrdinalIgnoreCase)) as WorksheetEntry;

            //Get the list feed of the worksheet
            var listFeedLink = worksheet.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);
            var query = new ListQuery(listFeedLink.HRef.ToString());
            return service.Query(query);
        }
    }
}
