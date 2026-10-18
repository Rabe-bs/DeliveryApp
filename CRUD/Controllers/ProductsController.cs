using CRUD.Data;
using CRUD.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    [ApiController]
    [Route("controller")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;

        public ProductsController(IProductRepository repo)
        {
            _repo = repo;

        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repo.GetAll();
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<int>> CrearProduct(Product prouduct)
        {
            await _repo.Add(prouduct);
            return Ok(prouduct.Id);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<int>> UpdatProduct(Product prouduct)
        {
            _repo.Update(prouduct);
            return Ok();
        }

        [HttpGet("ById")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var Recorde = await _repo.GetById(id);
            return Recorde == null ? NotFound() : Ok(Recorde);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeletProduct(int id)
        {
            await _repo.Delete(id);
            return Ok();


        }


    }
}
