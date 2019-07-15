using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using sbs_api.models;
using sbs_api.repository;

namespace sbs_api.Controllers
{
    [RoutePrefix("api/country")]
    public class CountryController : ApiController
    {
        private ICountryRepo repo;
        public CountryController()
        {
            repo = new CountryRepo();
        }

        // GET api/values
        [HttpGet]
        [Route("")]
        public IEnumerable<Country> Get()
        {
            return repo.GetCountries();
        }


        // POST api/values
        [HttpPost]
        [Route("{name}")]
        public void Post(string name)
        {
            repo.InsertCountry(name);
        }

        // PUT api/values/5
        [HttpPut()]
        [Route("{id}")]
        public void Put(int id, [FromBody] Country c)
        {
            repo.UpdateCountry(id, c.name);
        }

        // DELETE api/values/5
        [HttpDelete()]
        [Route("{id}")]
        public void Delete(int id)
        {
            repo.DeleteCountry(id);
        }
    }
}
