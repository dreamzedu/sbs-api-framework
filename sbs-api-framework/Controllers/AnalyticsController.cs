using System;
using System.Collections.Generic;
using System.Web.Http;
using sbs_api.models;
using sbs_api.repository;

namespace sbs_api.Controllers
{
    [RoutePrefix("api/analytics")]
    public class AnalyticsController : ApiController
    {
        private IAnalyticsRepo repo;

        public AnalyticsController()
        {
            repo = new AnalyticsRepo();
        }

        [HttpGet()]
        [Route("bydistrict/{distId:int}")]
        public IEnumerable<ResponsePercentage> Get(int distId)
        {
            return repo.GetDistrictWiseReport(distId);
        }
    }
}
