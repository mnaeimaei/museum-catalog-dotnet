using Museum.Collection.Catalog.Application.Dtos;
using Museum.Collection.Catalog.Domain.Entities;

namespace Museum.Collection.Catalog.Application.Mappings;

public static class ArtifactMappings
{
    public static ArtifactListItemDto ToListItemDto(this Artifact a)
        => new(a.Id, a.Category, a.Title, a.AccessionNumber);

    public static ArtifactEditionDto ToEditionDto(this ArtifactEdition e)
        => new(e.Id, e.ArtifactGuid, e.Version, e.Language, e.DisplayLabel);

    public static ArtifactDetailsDto ToDetailsDto(this Artifact a)
        => new(
            a.Id,
            a.Category,
            a.Title,
            a.Description,
            a.AccessionNumber,
            a.Editions.Select(e => e.ToEditionDto()).ToList()
        );
}