namespace Museum.Collection.Catalog.Application.Queries;

public sealed class GetArtifactsQuery
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public string? Title { get; set; }
    public string? AccessionNumber { get; set; }

    public string Sort { get; set; } = "title";
    public string Direction { get; set; } = "asc";

    public void Normalize()
    {
        // paging safety + defaults
        if (Page < 1) Page = 1;
        if (PageSize < 1) PageSize = 10;
        if (PageSize > 200) PageSize = 200;

        Sort = (Sort ?? "title").Trim().ToLowerInvariant();
        Direction = (Direction ?? "asc").Trim().ToLowerInvariant();

        Title = string.IsNullOrWhiteSpace(Title) ? null : Title.Trim();
        AccessionNumber = string.IsNullOrWhiteSpace(AccessionNumber) ? null : AccessionNumber.Trim();
    }
}