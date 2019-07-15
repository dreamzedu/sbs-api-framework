using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using sbs_api.models;
using sbs_api.repository;

namespace sbs_api.Controllers
{
    [RoutePrefix("api/state")]
    
    public class StateController : ApiController
    {
        private IStateRepo repo;
        public StateController()
        {
            repo = new StateRepo();
        }

        // GET api/values
        [HttpGet()]
        [Route("{countryId}")]
        public IEnumerable<State> Get(int countryId)
        {
            return repo.GetStates(countryId);
        }


        // POST api/values
        [HttpPost()]
        [Route("{countryId}")]
        public void Post(int countryId, [FromBody] State s)
        {
            repo.InsertState(s.name, countryId);
        }

        // PUT api/values/5
        [HttpPut()]
        [Route("{id}")]
        public void Put(int id, [FromBody] State s)
        {
            repo.UpdateState(id, s.name);
        }

        // DELETE api/values/5
        [HttpDelete()]
        [Route("{id}")]
        public void Delete(int id)
        {
            repo.DeleteState(id);
        }
    }
}
