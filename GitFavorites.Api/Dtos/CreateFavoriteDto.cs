using System.ComponentModel.DataAnnotations;

namespace GitFavorites.Api.Dtos;

public record CreateFavoriteDto
{
  [Required]
  public string? Name { get; init; }
  [Required]
  public string? Author { get; init; }
  public string? Description { get; init; }
  public string? Language { get; init; }
  public DateTime UpdatedAt { get; init; }
}