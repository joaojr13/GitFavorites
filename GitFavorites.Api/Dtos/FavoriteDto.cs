namespace GitFavorites.Api.Dtos;

public class FavoriteDto
{
  public Guid Id { get; init; }
  public string? Name { get; init; }
  public string? Description { get; init; }
  public string? Language { get; init; }
  public string? Author { get; init; }
  public DateTime UpdatedAt { get; init; }
}