using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using sbs_api.models;
using sbs_api.repository;

namespace sbs_api.Controllers
{
    [RoutePrefix("api/survey")]
    
    public class SurveyController : ApiController
    {
        private ISurveyRepo repo;
        public SurveyController()
        {
            repo = new SurveyRepo();
        }

        [HttpGet()]
        [Route("{startIndex}/{count}")]
        public IEnumerable<SurveyBasicDetail> Get(int startIndex, int count)
        {
            return repo.GetSurveys(startIndex, count);
        }

        [HttpGet()]
        [Route("{surveyId}")]
        public Survey GetSurvey(Int32 surveyId)
        {
            return repo.GetSurvey(surveyId);
        }

        [HttpGet()]
        [Route("{blockId}/{startIndex}/{count}")]
        public IEnumerable<SurveyBasicDetail> GetSurveysByBlock(int blockId, int startIndex, int count)
        {
            return repo.GetSurveysByBlock(blockId, startIndex, count);
        }

        [HttpGet()]
        [Route("{distId}/{startIndex}/{count}")]
        public IEnumerable<SurveyBasicDetail> GetSurveysByDistrict(int distId, int startIndex, int count)
        {
            return repo.GetSurveysByDistrict(distId, startIndex, count);
        }

        [HttpGet()]
        [Route("{panchayatId}/{startIndex}/{count}")]
        public IEnumerable<SurveyBasicDetail> GetSurveysByPanchayat(int panchayatId, int startIndex, int count)
        {
            return repo.GetSurveysByPanchayat(panchayatId, startIndex, count);
        }

        [HttpPost()]
        [Route("")]
        public void Post([FromBody] Survey s)
        {
            repo.InsertSurvey(s);
        }

        [HttpPost()]
        [Route("list")]
        public List<Int32> SaveAll([FromBody] List<Survey> surveys)
        {
            return repo.InsertSurveys(surveys);
        }


        // PUT api/values/5
        [HttpPost()]
        [Route("update")]
        public void UpdateSurvey([FromBody] Survey s)
        {
            repo.UpdateSurvey(s);
        }

        // DELETE api/values/5
        [HttpPost()]
        [Route("delete/{id}")]
        public void DeleteSurvey(Int32 id)
        {
            repo.DeleteSurvey(id);
        }
    }
}
