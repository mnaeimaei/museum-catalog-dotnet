using Microsoft.AspNetCore.Mvc;
using Museum.Collection.Catalog.Application.Queries;
using Museum.Collection.Catalog.Application.Services;

namespace Museum.Collection.Catalog.Api.Controllers;

[ApiController]
[Route("api/artifacts")]
public sealed class ArtifactsController : ControllerBase
{
    private readonly IArtifactService _service;

    public ArtifactsController(IArtifactService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetArtifacts([FromQuery] GetArtifactsQuery query)
        => Ok(await _service.GetArtifactsAsync(query));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var dto = await _service.GetByIdAsync(id);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpGet("{id:guid}/editions/{editionId:guid}")]
    public async Task<IActionResult> GetEdition(Guid id, Guid editionId)
    {
        var dto = await _service.GetEditionDtoAsync(id, editionId);
        return dto is null ? NotFound() : Ok(dto);
    }
}