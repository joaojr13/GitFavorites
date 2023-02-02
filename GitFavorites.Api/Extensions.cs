using GitFavorites.Api.Dtos;
using GitFavorites.Api.Entities;

namespace GitFavorites.Api;

public static class Extensions
{
    public static FavoriteDto AsDto(this Favorite favorite)
    {
        return new FavoriteDto()
        {
            Id = favorite.Id,
            Name = favorite.Name,
            Author = favorite.Author,
            Description = favorite.Description,
            Language = favorite.Language,
            UpdatedAt = favorite.UpdatedAt
        };
    }
}