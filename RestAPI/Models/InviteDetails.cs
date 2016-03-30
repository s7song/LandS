using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestAPI.Models
{
    public class InviteDetails
    {
        public string Url { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
        public bool MultiplePeople { get; set;}
        public bool? IsComing { get; set; }
        public string Remarks { get; set; }
        public bool InvitedToReception { get; set; }
    }
}