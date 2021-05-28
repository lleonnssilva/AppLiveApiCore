using ApiLives.Core.Domain.Entities.ApiLives;
using ApiLives.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace ApiLives.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LivesController : ControllerBase
    {
        private readonly ILiveRepository _liveRepo;
        public LivesController(
            ILiveRepository liveRepo
           )
        {
            _liveRepo = liveRepo;
            

        }

        [HttpGet("{id}", Name = "GetLive")]
        public async Task<ActionResult<Live>> GetById(int id)
        {
            var result =  await _liveRepo.GetById(id);
            var retorno = new
            {
                Content = result
            };

            return result;
        }
        [HttpDelete("{id}", Name = "DeleteLive")]
        public async Task<ActionResult> DeleteById(int id)
        {
            await _liveRepo.DeleteById(id);
           
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult<List<Live>>> GetByFlag(string flag)
        {
            var result = string.IsNullOrEmpty(flag) ? await _liveRepo.GetAll() :
                flag.Equals("next") ? await _liveRepo.GetByNext() :
                 flag.Equals("previous") ? await _liveRepo.GetByPrevious():
                  flag.Equals("today") ? await _liveRepo.GetByToday(): 
                  await _liveRepo.GetAll();
            var retorno = new
            {
                Content = result
             };

            return Ok(retorno);
        }



        [HttpPost]
        public async Task<ActionResult<dynamic>> PostLive([FromBody] Live model)
        {
            var result = await _liveRepo.Add(model);

            if (result == null)
                return NotFound(new { message = "Erro ao cadastrar!." });

            return Ok("Sucesso");

        }
        [HttpPut]
        public async Task<ActionResult<dynamic>> PutLive([FromBody] Live model)
        {
            var result = await _liveRepo.Update(model);

            if (result == null)
                return NotFound(new { message = "Erro ao cadastrar!." });

            return Ok("Sucesso");

        }
    }
}
