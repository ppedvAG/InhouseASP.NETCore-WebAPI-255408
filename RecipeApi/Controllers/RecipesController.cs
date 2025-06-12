using BusinessLogic.Contracts;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;
using RecipeApi.Mappers;

// Controller Namen sollten Plural sein und immer das Suffix "Controller" haben

namespace RecipeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IStaticRecipeService _recipeService;

        public RecipesController(IStaticRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        // GET: api/<RecipesController>
        [HttpGet]
        public ActionResult<IEnumerable<Recipe>> Get()
        {
            var result = _recipeService.GetAll();

            // OkResult gibt 200 OK zurück
            return Ok(result.Select(x => x.ToDto()));
        }

        // GET api/<RecipesController>/5
        [HttpGet("{id}")]
        public ActionResult<Recipe> Get(long id)
        {
            var result = _recipeService.GetById(id);
            return result is null 
                ? NotFound() // 404
                : Ok(result.ToDto()); // 200
        }

        // POST api/<RecipesController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateRecipeDto dto)
        {
            try
            {
                var result = _recipeService.Add(dto.ToDomainModel());
                return Ok(result);
            }
            catch (Exception)
            {
                // 500 Internal Server Error
                return new StatusCodeResult(500);
            }
        }

        // PUT api/<RecipesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] Recipe value)
        {
            try
            {
                var success = _recipeService.Update(value);
                return success ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                // 500 Internal Server Error
                return new StatusCodeResult(500);
            }
        }

        // DELETE api/<RecipesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                var success = _recipeService.Delete(id);
                return success ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                // 500 Internal Server Error
                return new StatusCodeResult(500);
            }
        }
    }
}
