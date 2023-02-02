using GitFavorites.Api.Entities;

namespace GitFavorites.Api.Interfaces;

public interface IMemoryRepository 
{
  Favorite? GetFavorite(Guid id);

  IEnumerable<Favorite> GetFavorites();

  void CreateFavorite(Favorite favorite);

  void DeleteFavorite(Guid id);
}