# Museum Collection Catalog API

A Clean Architecture ASP.NET Core Web API for managing museum artifacts and their exhibit editions.

This project demonstrates:

* Clean Architecture (Api / Domain / Infrastructure / Tests)
* Repository Pattern
* XML-based persistence
* Pagination, Sorting, Filtering
* xUnit test coverage
* RESTful API design with Swagger

---

# Domain Concept

This system models a museum cataloging platform.

Each **Artifact** can have multiple **Editions** (e.g., gallery labels, exhibit versions, translations).

### Artifact Structure

```text
artifact
 ├── id (GUID)
 ├── category (Painting / Sculpture / Manuscript / etc.)
 ├── title
 ├── accession_number
 ├── description
 └── editions
      ├── edition_id
      ├── artifact_guid
      ├── version (1, 1.1, 2.3)
      ├── language
      └── display_label
```

---

# Architecture

```
Museum.Collection.Catalog
 ├── Api              → Controllers & API configuration
 ├── Domain           → Entities & Interfaces
 ├── Infrastructure   → XML Repository implementation
 └── Tests            → xUnit tests
```

The system follows:

* Clean separation of concerns
* Dependency inversion
* Repository abstraction
* Infrastructure-driven persistence

---

# Features

## List Artifacts (Paged)

Returns:

* id
* category
* title
* accession_number

Supports:

* Pagination
* Sorting
* Filtering

---

## Sorting

Supports sorting by:

* title
* category
* accession_number

Ascending or descending.

---

## Filtering

Supports partial match filtering by:

* title
* accessionNumber

---

## Get Artifact by Id

Returns full details including:

* description
* editions (with version and language)

---

## Optional: Get Specific Edition

Fetch a specific edition using:

```
/api/artifacts/{artifactId}/editions/{editionId}
```

---

# How to Run the Backend

From the terminal:

```bash
cd backend/Museum.Collection.Catalog.Api
dotnet clean
dotnet build
dotnet run
```

Then open:

```
http://localhost:5052/swagger
```

---

# API Usage Examples (Outside Swagger)

## A. List Artifacts (Paged)

```
http://localhost:5052/api/artifacts
http://localhost:5052/api/artifacts?pageSize=2
http://localhost:5052/api/artifacts?page=3
http://localhost:5052/api/artifacts?page=3&pageSize=2
http://localhost:5052/api/artifacts?page=2&pageSize=2
```

---

## B. Sorting

```
http://localhost:5052/api/artifacts?sort=title
http://localhost:5052/api/artifacts?sort=title&direction=desc
http://localhost:5052/api/artifacts?sort=category
http://localhost:5052/api/artifacts?sort=category&direction=desc
http://localhost:5052/api/artifacts?sort=accession_number
http://localhost:5052/api/artifacts?sort=accession_number&direction=desc
```

---

## C. Searching / Filtering

```
http://localhost:5052/api/artifacts?title=fjord
http://localhost:5052/api/artifacts?accessionNumber=ACC-2026
http://localhost:5052/api/artifacts?title=fjord&accessionNumber=0001
```

---

## D. Artifact Details (Including Editions)

```
http://localhost:5052/api/artifacts/{artifactId}
```

Example:

```
http://localhost:5052/api/artifacts/1b0f6b9d-9f2e-4b1e-8f7e-7a7e4d7f0b6a
```

---

## E. Get Specific Edition

```
http://localhost:5052/api/artifacts/{artifactId}/editions/{editionId}
```

Example:

```
http://localhost:5052/api/artifacts/1b0f6b9d-9f2e-4b1e-8f7e-7a7e4d7f0b6a/editions/c2a0c4a1-56b7-4b1c-8c0e-7a5e9d2ef1a4
```

---

# Running Tests

```bash
cd backend/Museum.Collection.Catalog.Tests
dotnet test
```

---

# Persistence

Artifacts are stored in:

```
Infrastructure/Data/artifacts.xml
```

The system uses `XmlSerializer` for data loading and in-memory querying.

---

# Why This Project?

This project showcases:

* Strong C# and ASP.NET Core fundamentals
* Clean Architecture implementation
* SOLID principles
* Repository pattern
* REST API best practices
* Test-driven structure
* Separation between domain and infrastructure

---

# Tech Stack

* .NET 10
* ASP.NET Core Web API
* Swagger (Swashbuckle)
* xUnit
* XML Serialization
* LINQ

---

# Author

Milad Naeimaei
ASP.NET Core | Clean Architecture | Software Engineering