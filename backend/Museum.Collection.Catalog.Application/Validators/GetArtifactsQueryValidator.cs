using Museum.Collection.Catalog.Application.Queries;

namespace Museum.Collection.Catalog.Application.Validators;

public static class GetArtifactsQueryValidator
{
    public static void Validate(GetArtifactsQuery q)
    {
        if (q.Page < 1)
            throw new ArgumentException("Page must be >= 1");

        if (q.PageSize is < 1 or > 200)
            throw new ArgumentException("PageSize must be between 1 and 200");

        // Optional: lock sorting to allowed fields
        var allowedSort = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "title",
            "category",
            "accession_number",
            "accessionnumber"
        };

        if (!allowedSort.Contains(q.Sort))
            throw new ArgumentException("Sort must be one of: title, category, accession_number");

        if (!string.Equals(q.Direction, "asc", StringComparison.OrdinalIgnoreCase) &&
            !string.Equals(q.Direction, "desc", StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("Direction must be 'asc' or 'desc'");
    }
}