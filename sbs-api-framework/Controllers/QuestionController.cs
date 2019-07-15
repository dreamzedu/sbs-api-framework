using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using sbs_api.models;
using sbs_api.repository;

namespace sbs_api.Controllers
{
    [RoutePrefix("api/question")]
    
    public class QuestionController : ApiController
    {
        private IQuestionRepo repo;
        public QuestionController()
        {
            repo = new QuestionRepo();
        }

        // GET api/values
        [HttpGet]
        [Route("groupwise")]
        public IEnumerable<QuestionGroup> GetGroupWiseQuestions()
        {
            return repo.GetGroupWiseQuestions();
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Question> GetQuestions()
        {
            return repo.GetQuestions();
        }

        // GET api/values
        [HttpGet]
        [Route("headings")]
        public IEnumerable<Heading> GetQuestionHeadings()
        {
            return repo.GetQuestionHeadings();
        }

        // GET api/values
        [HttpGet()]
        [Route("answers")]
        public IEnumerable<PossibleAnswers> GetPossibleAnswers()
        {
            return repo.GetPossibleAnswers();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Question q)
        {
            repo.InsertQuestion(q);
        }

        // PUT api/values/5
        [HttpPost()]
        [Route("update/{id}")]
        public void UpdateQuestion(int id, [FromBody] Question q)
        {
            repo.UpdateQuestion(id, q);
        }

        // DELETE api/values/5
        [HttpPost()]
        [Route("delete/{id}")]
        public void DeleteQuestion(int id)
        {
            repo.DeleteQuestion(id);
        }
    }
}
