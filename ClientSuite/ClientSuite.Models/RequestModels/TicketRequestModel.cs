using System;
using System.Collections.Generic;
using System.Text;

namespace ClientSuite.Models.RequestModels
{
    public class TicketRequestModel: Ticket
    {
        public string Email { get; set; }
    }
}
