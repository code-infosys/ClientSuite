using ClientSuite.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientSuite.Models.RequestModels
{ 
    public class ClientRequestModel: User
    { 
        public int ProductId { get; set; } 
        public int PurchaseStatusId { get; set; }

        public DateTime FollowUpDate { get; set; }
        public int CategoryId { get; set; } 
         
    }
}
