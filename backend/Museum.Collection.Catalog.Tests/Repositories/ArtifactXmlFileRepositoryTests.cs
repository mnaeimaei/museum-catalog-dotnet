//ArtifactXmlFileRepositoryTests.cs
using Museum.Collection.Catalog.Infrastructure.Repositories;
using Xunit;

namespace Museum.Collection.Catalog.Tests.Repositories;

public sealed class ArtifactXmlFileRepositoryTests
{
    [Fact]
    public async Task Single_FindsExistingRecord_ById()
    {
        var repo = new XmlArtifactRepository(TestEnvironment.ArtifactsXmlFile);

        var artifact = await repo.SingleAsync(Guid.Parse("1b0f6b9d-9f2e-4b1e-8f7e-7a7e4d7f0b6a"));

        Assert.NotNull(artifact);
        Assert.Equal("The Fjord at Dawn", artifact!.Title);
        Assert.Equal("Painting", artifact.Category);
        Assert.Equal("ACC-2026-0001", artifact.AccessionNumber);
        Assert.NotEmpty(artifact.Editions);
        Assert.Equal(2, artifact.Editions.Count);
    }

    [Fact]
    public async Task Single_ReturnsNull_WhenIdNotFound()
    {
        var repo = new XmlArtifactRepository(TestEnvironment.ArtifactsXmlFile);

        var artifact = await repo.SingleAsync(Guid.NewGuid());

        Assert.Null(artifact);
    }

    [Fact]
    public async Task Where_Filters_ByTitle()
    {
        var repo = new XmlArtifactRepository(TestEnvironment.ArtifactsXmlFile);

        var actual = await repo.WhereAsync(a =>
            a.Title.Equals("Bronze Runner", StringComparison.CurrentCultureIgnoreCase));

        Assert.Single(actual);
        Assert.Equal("2c9c2f4d-7a91-4f2e-8d0b-1e8a5a0d3a4f", actual.Single().Id.ToString());
    }

    [Fact]
    public async Task Where_WithOrderBy_OrdersArtifacts()
    {
        var repo = new XmlArtifactRepository(TestEnvironment.ArtifactsXmlFile);

        var actual = await repo.WhereAsync(
            a => a.Category.Equals("Painting", StringComparison.CurrentCultureIgnoreCase)
              || a.Category.Equals("Sculpture", StringComparison.CurrentCultureIgnoreCase),
            artifacts => artifacts.OrderBy(a => a.Title)
        );

        Assert.NotEmpty(actual);

        // "Bronze Runner" should come before "The Fjord at Dawn"
        Assert.Equal("Bronze Runner", actual.First().Title);
    }

    [Fact]
    public async Task GetByCategory_ReturnsMatching()
    {
        var repo = new XmlArtifactRepository(TestEnvironment.ArtifactsXmlFile);

        var actual = await repo.GetByCategoryAsync("Manuscript");

        Assert.Single(actual);
        Assert.Equal("Mariner’s Log, 1887", actual.Single().Title);
    }

    [Fact]
    public async Task GetByAccessionNumber_ReturnsMatching()
    {
        var repo = new XmlArtifactRepository(TestEnvironment.ArtifactsXmlFile);

        var actual = await repo.GetByAccessionNumberAsync("ACC-2026-0007");

        Assert.Single(actual);
        Assert.Equal("Coastal Survey Chart", actual.Single().Title);
    }

    [Fact]
    public async Task GetEdition_ReturnsEdition_WhenItBelongsToArtifact()
    {
        var repo = new XmlArtifactRepository(TestEnvironment.ArtifactsXmlFile);

        var artifactId = Guid.Parse("2c9c2f4d-7a91-4f2e-8d0b-1e8a5a0d3a4f"); // Bronze Runner
        var editionId  = Guid.Parse("6a2f9d0c-1b7e-4c5a-8e2d-0f9a1c2b3d4e"); // v2.1

        var edition = await repo.GetEditionAsync(artifactId, editionId);

        Assert.NotNull(edition);
        Assert.Equal("2.1", edition!.Version);
        Assert.Equal("en-US", edition.Language);
        Assert.Equal("Bronze Runner — Gallery Label v2.1", edition.DisplayLabel);
        Assert.Equal(artifactId, edition.ArtifactGuid);
    }

    [Fact]
    public async Task GetEdition_ReturnsNull_WhenEditionDoesNotExist()
    {
        var repo = new XmlArtifactRepository(TestEnvironment.ArtifactsXmlFile);

        var artifactId = Guid.Parse("2c9c2f4d-7a91-4f2e-8d0b-1e8a5a0d3a4f");

        var edition = await repo.GetEditionAsync(artifactId, Guid.NewGuid());

        Assert.Null(edition);
    }
}

public static class TestEnvironment
{
    // Adjust folders to match your real repo layout:
    // Your tree shows: Museum.Collection.Catalog.Infrastructure/Data/artifacts.xml
    public static readonly string ArtifactsXmlFile =
        Path.GetFullPath(
            Path.Combine(
                Directory.GetCurrentDirectory(),
                "..", "..", "..", "..",              // from Tests/bin/... to solution root (often 4 levels)
                "Museum.Collection.Catalog.Infrastructure",
                "Data",
                "artifacts.xml"
            )
        );
}
