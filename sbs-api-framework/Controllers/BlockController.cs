using System;
using System.Collections.Generic;
using System.Web.Http;
using sbs_api.models;
using sbs_api.repository;

namespace sbs_api.Controllers
{
    [RoutePrefix("api/block")]
    public class BlockController : ApiController
    {
        private IBlockRepo repo;
        public BlockController()
        {
            repo = new BlockRepo();
        }

        [HttpGet()]
        [Route("{distId:int}")]
        public IEnumerable<Block> Get(int distId)
        //public string Get(int distId)
        {
            List<Block> blocks = new List<Block>();
            blocks.Add(new Block(){id=1, name = "test1"});
            //return "Test passed";
            return repo.GetBlocks(distId);
        }


        // POST api/values
        [HttpPost()]
        [Route("{distId:int}")]
        public int Post(int distId, [FromBody] Block block)
        {
            return repo.InsertBlock(block.name, distId);
        }

        // PUT api/values/5
        [HttpPost()]
        [Route("update/{id:int}")]
        public void UpdateBlock(int id, [FromBody] Block block)
        {
            repo.UpdateBlock(id, block.name);
        }

        // DELETE api/values/5
        [HttpPost()]
        [Route("delete/{id:int}")]
        public void DeleteBlock(int id)
        {
            repo.DeleteBlock(id);
        }
    }
}
