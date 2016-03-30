using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.GData.Client;
using Google.GData.Spreadsheets;
using RestAPI.Models;

namespace RestAPI.Controllers
{
    [RoutePrefix("Invite")]
    public class InviteController : ApiController
    {
        [Route("{uniqueUrl}"), HttpGet]
        public async Task<IHttpActionResult> GetInvitation(string uniqueUrl)
        {
            var service = GetSpreadSheetService();
            var feed = GetListFeed(service, String.Format("url = {0}", uniqueUrl));

            var inviteDetails = feed.Entries.Cast<ListEntry>().Select(ToInviteDetails).FirstOrDefault();

            return Ok(inviteDetails);
        }

        [Route("{uniqueUrl}"), HttpPost]
        public async Task<IHttpActionResult> Rsvp(string uniqueUrl, [FromBody] InviteDetails inviteDetails)
        {
            var service = GetSpreadSheetService();
            var feed = GetListFeed(service, String.Format("url = {0}", uniqueUrl));

            var entry = feed.Entries.Cast<ListEntry>().FirstOrDefault();
            foreach (ListEntry.Custom column in entry.Elements)
            {
                if (String.Equals(column.LocalName, "comingyesno", StringComparison.OrdinalIgnoreCase))
                    column.Value = inviteDetails.IsComing == null ? null : inviteDetails.IsComing.Value ? "yes" : "no";
                else if (String.Equals(column.LocalName, "remarks", StringComparison.OrdinalIgnoreCase))
                    column.Value = inviteDetails.Remarks;
            }
            service.Update(entry);
            return Ok();
        }

        private InviteDetails ToInviteDetails(ListEntry entry)
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
                else if (String.Equals(column.LocalName, "invitetoreception", StringComparison.OrdinalIgnoreCase))
                    inviteDetails.InvitedToReception = !String.IsNullOrWhiteSpace(column.Value) && String.Equals(column.Value, "yes", StringComparison.OrdinalIgnoreCase);
            }

            return inviteDetails;
        }

        private SpreadsheetsService GetSpreadSheetService()
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

            var cred = new UserCredential(flow, "user", new Google.Apis.Auth.OAuth2.Responses.TokenResponse
            {
                AccessToken = "ya29.kQIfpYVq0nzmLEtsGTGGYiDsjAx6Zj0dh7YIJRh0LWSjoCS_H2IV-4EHDFBvZLME7o-A",
                RefreshToken = "1/JdE7vwhJpgwg_fqN26NaAl3bJNVnYIAsfHdjSzRv9YM"
            });

            var oauth = new OAuth2Parameters
            {
                ClientId = clientSecrets.ClientId,
                ClientSecret = clientSecrets.ClientSecret,
                Scope = String.Join(" ", scopes),
                AccessToken = cred.Token.AccessToken,
                RefreshToken = cred.Token.RefreshToken,
            };

            var applicationName = "wedding-project-1140";
            var requestFactory = new GOAuth2RequestFactory(null, applicationName, oauth);

            return new SpreadsheetsService(applicationName) { RequestFactory = requestFactory };
        }

        private ListFeed GetListFeed(SpreadsheetsService service, string queryString)
        {
            var spreadsheet = service.Get("https://spreadsheets.google.com/feeds/spreadsheets/private/full/1_8yyQMFVtpOtQW7F2_1PpoVbJU981itTzNljiuUgrDw") as SpreadsheetEntry;
            var worksheet = spreadsheet.Worksheets.Entries.FirstOrDefault(x => x.Title.Text.Equals("Website", StringComparison.OrdinalIgnoreCase)) as WorksheetEntry;

            //Get the list feed of the worksheet
            var listFeedLink = worksheet.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);
            var query = new ListQuery(listFeedLink.HRef.ToString()) { SpreadsheetQuery = queryString };
            return service.Query(query);
        }
    }
}
