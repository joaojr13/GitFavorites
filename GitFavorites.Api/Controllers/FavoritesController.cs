using GitFavorites.Api.Dtos;
using GitFavorites.Api.Entities;
using GitFavorites.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GitFavorites.Api.Controllers;

[ApiController]
[Route("favorites")]
public class FavoritesController : ControllerBase
{
  private readonly IMemoryRepository _repository;

  public FavoritesController(IMemoryRepository repository)
  {
    _repository = repository;
  }

  [HttpGet]
  public IEnumerable<FavoriteDto> GetFavorites()
  {
    var favorites = _repository.GetFavorites().Select(favorite => favorite.AsDto());
    return favorites;
  }

  [HttpGet("{id}")]
  public ActionResult<FavoriteDto> GetFavorite(Guid id)
  {
    var favorite = _repository.GetFavorite(id);
    
    if(favorite is null)
      return NotFound();
    
    return favorite.AsDto();
  }

  [HttpPost]
  public ActionResult<FavoriteDto> CreateFavorite([FromBody] CreateFavoriteDto newFavorite)
  {   
      Favorite favorite = new() 
      {
        Id = Guid.NewGuid(),
        Name = newFavorite.Name,
        Author = newFavorite.Author,
        Description = newFavorite.Description,
        Language = newFavorite.Language,
        UpdatedAt = newFavorite.UpdatedAt
      };

      _repository.CreateFavorite(favorite);

      return CreatedAtAction(nameof(GetFavorite), new { id = favorite.Id }, favorite.AsDto());
  }

  [HttpDelete("{id}")]
  public ActionResult DeleteFavorite(Guid id)
  {
      var favorite = _repository.GetFavorite(id);

      if(favorite is null)
        return NotFound();

      _repository.DeleteFavorite(id);

      return NoContent();
  }
}