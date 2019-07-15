using System;
using System.Collections.Generic;
using System.Web.Http;
using sbs_api.models;
using sbs_api.repository;
using sbs_api.repository.Interfaces;

namespace sbs_api.Controllers
{
    [RoutePrefix("api/dataversion")]
    public class DataVersionController : ApiController
    {
        private IDataVersionRepo repo;

        public DataVersionController()
        {
            repo = new DataVersionRepo();
        }

        [HttpGet()]
        public IEnumerable<DataVersion> Get()
        {
            return repo.GetDataVersions();
        }
    }
}
