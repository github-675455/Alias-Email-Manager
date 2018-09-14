using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using AliasManager.Model;
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

        [HttpGet("")]
        public IList<Aliases> Index()
        {
            return aliasesRepository.FindAll();
        }
        
        [HttpPost("create")]
        public IActionResult Create([FromBody]Aliases alias)
        {
            if (!ModelState.IsValid)
                return Forbid(string.Format("{0} não é valido.", alias.GetType().Name));

            var aliasEncontrado = aliasesRepository.FindByAlias(alias.Alias);



            return Ok(aliasesRepository.Add(alias));
        }
        
        [HttpPost("edit")]
        public IActionResult Edit([FromBody]Aliases alias)
        {
            if (!ModelState.IsValid)
                return Forbid(string.Format("{0} não é valido.", alias.GetType().Name));

            aliasesRepository.Update(alias);

            return Ok(alias);
        }
        
        [HttpDelete("delete")]
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