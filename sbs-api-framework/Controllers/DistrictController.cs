using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using sbs_api.models;
using sbs_api.repository;

namespace sbs_api.Controllers
{
    [RoutePrefix("api/district")]
    
    public class DistrictController : ApiController
    {
        private IDistrictRepo repo;
        public DistrictController(IDistrictRepo districtRepo)
        {
            repo = districtRepo;
        }

        // GET api/values
        [HttpGet()]
        [Route("{stateId}")]
        public IEnumerable<District> Get(int stateId)
        {
            return repo.GetDistricts(stateId);
        }


        // POST api/values
        [HttpPost()]
        [Route("{stateId}")]
        public void Post(int stateId, [FromBody] District d)
        {
            repo.InsertDistrict(d.name, stateId);
        }

        // PUT api/values/5
        [HttpPut()]
        [Route("{id}")]
        public void Put(int id, [FromBody] District d)
        {
            repo.UpdateDistrict(id, d.name);
        }

        // DELETE api/values/5
        [HttpDelete()]
        [Route("{id}")]
        public void Delete(int id)
        {
            repo.DeleteDistrict(id);
        }
    }
}
