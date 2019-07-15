using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using sbs_api.models;
using sbs_api.repository;

namespace sbs_api.Controllers
{
    [RoutePrefix("api/panchayat")]
    
    public class PanchayatController : ApiController
    {
        private IPanchayatRepo repo;
        public PanchayatController()
        {
            repo = new PanchayatRepo();
        }

        // GET api/values
        [HttpGet()]
        [Route("{blockId}")]
        public IEnumerable<Panchayat> Get(int blockId)
        {
            return repo.GetPanchayats(blockId);
        }

        [HttpGet()]
        [Route("all")]
        public IEnumerable<Panchayat> GetAll()
        {
            return repo.GetPanchayats();
        }


        // POST api/values
        [HttpPost()]
        [Route("{blockId}")]
        public void Post(int blockId, [FromBody] Panchayat v)
        {
            repo.InsertPanchayat(v.name, blockId);
        }

        // PUT api/values/5
        [HttpPost()]
        [Route("update/{id}")]
        public void UpdatePanchayat(int id, [FromBody] Panchayat v)
        {
            repo.UpdatePanchayat(id, v.name);
        }

        // DELETE api/values/5
        [HttpPost()]
        [Route("delete/{id}")]
        public void DeletePanchayat(int id)
        {
            repo.DeletePanchayat(id);
        }
    }
}
