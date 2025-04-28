using LibraryManagement.Domain.Enums;
using LibraryManagement.Service.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers;

[Route("api/[controller]")] 
[Authorize]
[ApiController]
public class CategoryController : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = $"{Roles.Member},{Roles.SupperAdmin}")]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetGenres()
    {
        // TODO: Implement logic to retrieve all genres
        // Example:
        // var genres = await _genreService.GetAllAsync();
        // return Ok(genres);
        await Task.CompletedTask; // Simulate async work
        throw new NotImplementedException();
    }
    
    [HttpGet("{id:guid}")]
    [Authorize(Roles = $"{Roles.Member},{Roles.SupperAdmin}")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<CategoryDto>> GetGenre(Guid id)
    {
        // TODO: Implement logic to retrieve a genre by ID
        // Example:
        // var genre = await _genreService.GetByIdAsync(id);
        // if (genre == null) return NotFound();
        // return Ok(genre);
        await Task.CompletedTask; // Simulate async work
        throw new NotImplementedException();
    }
    
    [HttpPost]
    [Authorize(Roles = Roles.SupperAdmin)]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<CategoryDto>> PostGenre(CreateCategoryDto createCategoryDto) // Assuming CreateCategoryDto exists
    {
        // TODO: Implement logic to create a new genre
        // Example:
        // if (!ModelState.IsValid) return BadRequest(ModelState);
        // var newGenre = await _genreService.CreateAsync(createCategoryDto);
        // return CreatedAtAction(nameof(GetGenre), new { id = newGenre.Id }, newGenre);
        await Task.CompletedTask; // Simulate async work
        throw new NotImplementedException();
    }
    
    [HttpPut("{id:guid}")]
    [Authorize(Roles = Roles.SupperAdmin)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> PutGenre(Guid id, UpdateCategoryDto updateCategoryDto) // Assuming UpdateCategoryDto exists
    {
        // TODO: Implement logic to update a genre
        // Example:
        // if (!ModelState.IsValid) return BadRequest(ModelState);
        // var success = await _genreService.UpdateAsync(id, updateCategoryDto);
        // if (!success) return NotFound();
        // return NoContent();
        await Task.CompletedTask; // Simulate async work
        throw new NotImplementedException();
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = Roles.SupperAdmin)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteGenre(Guid id)
    {
        // TODO: Implement logic to delete a genre (consider soft delete)
        // Example:
        // var success = await _genreService.DeleteAsync(id);
        // if (!success) return NotFound();
        // return NoContent();
        await Task.CompletedTask; // Simulate async work
        throw new NotImplementedException();
    }
}