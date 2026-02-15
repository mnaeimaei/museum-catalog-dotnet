//ArtifactsController.cs
using Microsoft.AspNetCore.Mvc;
using Museum.Collection.Catalog.Domain.Interfaces;

namespace Museum.Collection.Catalog.Api.Controllers;

[ApiController]
[Route("api/artifacts")]
public sealed class ArtifactsController : ControllerBase
{
    private readonly IArtifactRepository _repo;

    public ArtifactsController(IArtifactRepository repo)
    {
        _repo = repo;
    }

    // GET /api/artifacts?page=1&pageSize=10&title=fjord&accessionNumber=ACC-2026&sort=title&direction=asc
    [HttpGet]
    public async Task<IActionResult> GetArtifacts(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? title = null,
        [FromQuery] string? accessionNumber = null,
        [FromQuery] string? sort = "title",
        [FromQuery] string? direction = "asc")
    {
        // Filter (partial match)
        var items = await _repo.WhereAsync(a =>
            (title == null || a.Title.Contains(title, StringComparison.OrdinalIgnoreCase)) &&
            (accessionNumber == null || a.AccessionNumber.Contains(accessionNumber, StringComparison.OrdinalIgnoreCase))
        );

        // Sort
        var desc = string.Equals(direction, "desc", StringComparison.OrdinalIgnoreCase);

        items = (sort?.ToLowerInvariant()) switch
        {
            "title" => desc ? items.OrderByDescending(a => a.Title).ToList() : items.OrderBy(a => a.Title).ToList(),
            "category" => desc ? items.OrderByDescending(a => a.Category).ToList() : items.OrderBy(a => a.Category).ToList(),
            "accession_number" or "accessionnumber" => desc
                ? items.OrderByDescending(a => a.AccessionNumber).ToList()
                : items.OrderBy(a => a.AccessionNumber).ToList(),
            _ => items
        };

        // Paging safety
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;

        var total = items.Count;
        var paged = items.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        // Return only the mandatory list fields
        return Ok(new
        {
            page,
            pageSize,
            total,
            data = paged.Select(a => new
            {
                id = a.Id,
                category = a.Category,
                title = a.Title,
                accession_number = a.AccessionNumber
            })
        });
    }

    // GET /api/artifacts/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var artifact = await _repo.SingleAsync(id);
        if (artifact is null)
            return NotFound();

        // Return full details + editions (mapped to your required fields)
        return Ok(new
        {
            id = artifact.Id,
            category = artifact.Category,
            title = artifact.Title,
            description = artifact.Description,
            accession_number = artifact.AccessionNumber,
            editions = artifact.Editions.Select(e => new
            {
                edition_id = e.Id,
                artifact_guid = e.ArtifactGuid,
                version = e.Version,
                language = e.Language,
                display_label = e.DisplayLabel
            })
        });
    }

    // OPTIONAL: GET /api/artifacts/{id}/editions/{editionId}
    [HttpGet("{id:guid}/editions/{editionId:guid}")]
    public async Task<IActionResult> GetEdition([FromRoute] Guid id, [FromRoute] Guid editionId)
    {
        var edition = await _repo.GetEditionAsync(id, editionId);
        if (edition is null)
            return NotFound();

        return Ok(new
        {
            edition_id = edition.Id,
            artifact_guid = edition.ArtifactGuid,
            version = edition.Version,
            language = edition.Language,
            display_label = edition.DisplayLabel
        });
    }
}
