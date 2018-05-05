using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using AliasManager.Models;
using AliasManager.Repository;

namespace AliasManager.Controllers
{
    [Produces("application/json")]
    [Route("api/aliases")]
    public class AliasesController : Controller
    {
        private readonly AliasesRepository aliasesRepository;

        public AliasesController(IConfiguration configuration)
        {
            aliasesRepository = new AliasesRepository(configuration);
        }

        [HttpGet]
        public IList<Aliases> Index()
        {
            return aliasesRepository.FindAll();
        }
        
        [HttpPost]
        public IActionResult Create([FromBody]Aliases alias)
        {
            if (!ModelState.IsValid)
                return Forbid(string.Format("{0} não é valido.", alias.GetType().Name.ToString()));
            
            return Ok(aliasesRepository.Add(alias));
        }
        
        [HttpPost]
        public IActionResult Edit([FromBody]Aliases alias)
        {
            if (!ModelState.IsValid)
                return Forbid(string.Format("{0} não é valido.", alias.GetType().Name.ToString()));

            aliasesRepository.Update(alias);

            return Ok(alias);
        }
        
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            aliasesRepository.Remove(id.Value);
            return Ok();
        }
    }
}