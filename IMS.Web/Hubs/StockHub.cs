using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace IMS.Web.Hubs
{
    public class StockHub : Hub
    {

        public void Subscribe()
        {            
            Groups.Add(Context.ConnectionId, "Restaurant");
        }

        public void Unsubscribe()
        {
            Groups.Remove(Context.ConnectionId, "Restaurant");
        }
        
    }
}