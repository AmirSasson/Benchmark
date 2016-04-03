using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace StateMachineTests.Api
{
    public class AccountController : ApiController
    {
        IRepo<UserProps> _repo;
        ILogger _logger;
        public AccountController(IRepo<UserProps> repo, ILogger logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // GET api/values 
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5 
        public UserProps Get(long id)
        {
            _logger.Log($"Get {id}..");
            return _repo.Get(new { Id = id });

        }

        // POST api/values 
        public void Post([FromBody]UserProps value)
        {
            _logger.Log($"Post {value.Id}..");
            _repo.Set(value);

        }

        // PUT api/values/5 
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5 
        public void Delete(int id)
        {
        }
    }
}
