using FluentAssertions;
using GitFavorites.Api.Controllers;
using GitFavorites.Api.Dtos;
using GitFavorites.Api.Entities;
using GitFavorites.Api.Interfaces;
using GitFavorites.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GitFavorites.Tests;

public class FavoritesControllerTests
{
  [Fact]
  public void GetFavorites_ExistingFavorites_ReturnsAllFavorites()
  {
    var expectedFavorites = CreateNewListOfFavorites();

    IMemoryRepository repositoryMock = new MemoryRepository();
    expectedFavorites.ForEach(favorite => repositoryMock.CreateFavorite(favorite));

    var controller = new FavoritesController(repositoryMock);

    var result = controller.GetFavorites();

    result.Should().BeEquivalentTo(
        expectedFavorites,
        options => options.ComparingByMembers<Favorite>());
  }

  [Fact]
  public void GetFavorite_UnexistingFavorite_ReturnsNotFound()
  {
    IMemoryRepository repositoryMock = new MemoryRepository();

    var controller = new FavoritesController(repositoryMock);

    var result = controller.GetFavorite(Guid.NewGuid());

    result.Result.Should().BeOfType<NotFoundResult>();
  }

  [Fact]
  public void GetFavorite_ExistingFavorite_ReturnsExpectedFavorite()
  {
    var expectedFavorite = CreateNewFavorite();

    IMemoryRepository repositoryMock = new MemoryRepository();
    repositoryMock.CreateFavorite(expectedFavorite);

    var controller = new FavoritesController(repositoryMock);

    var result = controller.GetFavorite(expectedFavorite.Id);

    result.Value.Should().BeEquivalentTo(
        expectedFavorite,
        options => options.ComparingByMembers<Favorite>());
  }

  [Fact]
  public void CreateFavorite_FavoriteToCreate_ReturnsCreatedFavorite()
  {
    IMemoryRepository repositoryMock = new MemoryRepository();

    var favoriteToCreate = new CreateFavoriteDto()
    {
      Author = "joaojr13",
      Description = "New Repository of React",
      Language = "Typescript",
      Name = "new-react-app",
      UpdatedAt = DateTime.Now
    };

    var controller = new FavoritesController(repositoryMock);

    var result = controller.CreateFavorite(favoriteToCreate);

    var createdItem = (result.Result as CreatedAtActionResult).Value as FavoriteDto;

    favoriteToCreate.Should().BeEquivalentTo(
        createdItem,
        options => options.ComparingByMembers<FavoriteDto>()
                            .ExcludingMissingMembers());
  }

  [Fact]
  public void DeleteFavorite_UnexistingFavorite_ReturnsNotFound()
  {
    IMemoryRepository repositoryMock = new MemoryRepository();

    var controller = new FavoritesController(repositoryMock);

    var result = controller.DeleteFavorite(Guid.NewGuid());

    result.Should().BeOfType<NotFoundResult>();
  }

  [Fact]
  public void DeleteFavorite_ExistingFavorite_ReturnsNoContent()
  {
    var favoriteToDelete = CreateNewFavorite();

    IMemoryRepository repositoryMock = new MemoryRepository();
    repositoryMock.CreateFavorite(favoriteToDelete);

    var controller = new FavoritesController(repositoryMock);

    var result = controller.DeleteFavorite(favoriteToDelete.Id);

    result.Should().BeOfType<NoContentResult>();
  }

  private Favorite CreateNewFavorite()
  {
    return new Favorite()
    {
      Id = Guid.NewGuid(),
      Name = "Project-Tests",
      Author = "joaojr13",
      Description = "Units tests for a c# project",
      Language = "C#",
      UpdatedAt = DateTime.Now
    };
  }

  private List<Favorite> CreateNewListOfFavorites()
  {
    return new List<Favorite>()
    {
        CreateNewFavorite(),
        CreateNewFavorite(),
        CreateNewFavorite(),
    };
  }
}