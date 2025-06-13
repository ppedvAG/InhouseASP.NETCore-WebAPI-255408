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
        private readonly IStaticRecipeService _recipeService;

        public RecipesController(IStaticRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        // GET: api/<RecipesController>
        [HttpGet]
        public ActionResult<IEnumerable<Recipe>> Get([FromQuery] RecipeSearchParams filterParams)
        {
            var result = _recipeService.GetAll();

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
        public ActionResult<Recipe> Get(long id)
        {
            var result = _recipeService.GetById(id);
            return result is null 
                ? NotFound() // 404
                : Ok(result.ToDto()); // 200
        }

        // POST api/<RecipesController>
        [HttpPost]
        public IActionResult Post(CreateRecipeDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = _recipeService.Add(dto.ToDomainModel());
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
        [HttpDelete]
        [Route("{id:long}")]
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
