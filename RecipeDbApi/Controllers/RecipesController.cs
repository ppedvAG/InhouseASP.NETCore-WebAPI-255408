using BusinessLogic.Contracts;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;
using RecipeDbApi.Mappers;

// Controller Namen sollten Plural sein und immer das Suffix "Controller" haben

namespace RecipeDbApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        // GET: api/<RecipesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> Get([FromQuery] RecipeSearchParams filterParams)
        {
            IEnumerable<Recipe> result = await _recipeService.GetAll();

            // TODO Filterung sollte natuerlich in der BusinessLogic liegen
            if (filterParams.Difficulty != null)
            {
                result = result.Where(x => x.Difficulty.Equals(filterParams.Difficulty));
            }

            if (!string.IsNullOrEmpty(filterParams.MealType))
            {
                result = result.Where(x => x.MealType.Contains(filterParams.MealType));
            }

            if (!string.IsNullOrEmpty(filterParams.Cuisine))
            {
                result = result.Where(x => x.Cuisine.Contains(filterParams.Cuisine));
            }

            // OkResult gibt 200 OK zurück
            return Ok(result.Select(x => x.ToDto()).ToList());
        }

        // GET api/<RecipesController>/5
        [HttpGet]
        [Route("{id:long}")]
        public async Task<ActionResult<Recipe>> Get(long id)
        {
            var result = await _recipeService.GetById(id);
            return result is null 
                ? NotFound() // 404
                : Ok(result.ToDto()); // 200
        }

        // POST api/<RecipesController>
        [HttpPost]
        public async Task<IActionResult> Post(CreateRecipeDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var id = await _recipeService.Add(dto.ToDomainModel());
                var result = await _recipeService.GetById(id);
                return Ok(result);
            }
            // Best Practice: Nicht den Basistyp Exception abfangen sondern
            // spezifische Exception Types
            catch (HttpIOException ex) when (ex.Source == "file")
            {
                // 500 Internal Server Error
                return new StatusCodeResult(500);
            }
            catch (Exception)
            {
                // 500 Internal Server Error
                return new StatusCodeResult(500);
            }
        }

        // PUT api/<RecipesController>/5
        [HttpPut]
        [Route("{id:long}")]
        public async Task<IActionResult> Put(long id, [FromBody] Recipe value)
        {
            try
            {
                var success = await _recipeService.Update(value);
                return success ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                // 500 Internal Server Error
                return new StatusCodeResult(500);
            }
        }

        // DELETE api/<RecipesController>/5
        [HttpDelete]
        [Route("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var success = await _recipeService.Delete(id);
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
