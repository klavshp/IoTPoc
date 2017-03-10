using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RfidWebApi.Models
{
    public class RfidData
    {
        public string DeviceId { get; set;}

        public string DeviceName { get; set; }

        public DateTime Datetime { get; set; }

        public string RfidTag { get; set; }
    }
}