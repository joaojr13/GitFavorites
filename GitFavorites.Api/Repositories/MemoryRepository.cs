using GitFavorites.Api.Entities;
using GitFavorites.Api.Interfaces;

namespace GitFavorites.Api.Repositories;

public class MemoryRepository : IMemoryRepository
{
  private readonly List<Favorite> favorites = new();

  public IEnumerable<Favorite> GetFavorites()
  {
      return favorites;
  }

  public Favorite? GetFavorite(Guid id)
  {
      return favorites.Where(favorite => favorite.Id == id).SingleOrDefault();
  }

  public void CreateFavorite(Favorite favorite)
  {
    favorites.Add(favorite);
  }

  public void DeleteFavorite(Guid id)
  {
    var index = favorites.FindIndex(favorite => favorite.Id == id);
    favorites.RemoveAt(index);
  }
}