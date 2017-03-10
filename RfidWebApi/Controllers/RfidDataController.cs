using System.Collections.Generic;
using System.Web.Http;
using Config;
//using RfidWebApi.Models;

using RfidWebApi.Repositories;

namespace RfidWebApi.Controllers
{
    public class RfidDataController : ApiController
    {
        public IEnumerable<RfidData> GetAllRfidData()
        {
            var rfidDataRepository = new RfidDataRepository();
            var data = rfidDataRepository.GetAllRfidData();
            return data;
        }
    }
}
