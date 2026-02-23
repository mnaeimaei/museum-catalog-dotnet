# Museum Collection Catalog

The **Museum Collection Catalog** is a web-based application composed of a backend API and a frontend client. It is designed to support museums in organizing, managing, and presenting information about their collections in a structured and consistent manner.

The system enables institutions to catalog artifacts, maintain descriptive metadata, and manage multiple exhibit editions associated with each artifact.

---

## Artifact Concept

Within this system, an **artifact** refers to a formally cataloged object that belongs to a museum’s collection. An artifact may represent a physical, historical, or cultural object such as a painting, sculpture, manuscript, map, textile, piece of jewelry, audio recording, or any other item of cultural significance preserved by the institution.

Each artifact is associated with structured metadata (e.g., title, category, accession number, and description) and may include multiple **editions**. Editions represent different exhibition labels, catalog versions, translations, or interpretative materials prepared for display or publication.

Each **Artifact** can have multiple **Editions**, for example:

* Gallery labels
* Exhibit versions
* Language translations

---

### Artifacts Conceptual Model

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

# Features

The Museum Collection Catalog API provides structured access to artifact data with the following capabilities:

* **Paginated listing of artifacts**, returning core fields (id, category, title, accession number).
* **Sorting support** by title, category, or accession number (ascending or descending).
* **Flexible filtering** using partial matches on title and accession number.
* **Detailed artifact retrieval** by identifier, including full metadata and associated editions (version and language information).
* **Optional edition retrieval**, allowing access to a specific edition of an artifact via its unique identifier.

---

# Backend API Architecture

The backend is implemented as a **Clean Architecture–based ASP.NET Core Web API**.

The architectural design emphasizes:

* Clear separation of concerns
* Dependency inversion
* Repository abstraction
* Infrastructure-based persistence
* SOLID principles
* REST API best practices
* Testability and maintainability

### Technology Stack

* .NET 10
* ASP.NET Core Web API
* Swagger (Swashbuckle)
* xUnit
* XML Serialization
* LINQ

### Backend Structure

```text
backend
 ├── Api              → Controllers and API configuration
 ├── Domain           → Core entities and interfaces
 ├── Infrastructure   → XML-based repository implementation
 └── Tests            → xUnit test project
```

The artifact data file (`artifacts.xml`) is located in:

```
backend/Museum.Collection.Catalog.Infrastructure/Data
```

This structure ensures maintainability, extensibility, and testability of the system.

---

# Frontend Web Application Architecture

The frontend is implemented as a **React-based Single Page Application (SPA)**.

It provides a user interface for:

* Browsing artifacts
* Viewing artifact details and editions
* Searching and sorting catalog entries

### Frontend Structure

```text
└── frontend
    ├── node_modules
    ├── public
    │   └── vite.svg
    ├── src
    │   ├── index.css
    │   ├── main.jsx
    │   ├── assets
    │   │   └── react.svg
    │   ├── app
    │   │   ├── App.jsx
    │   │   ├── routes.jsx
    │   │   ├── layout
    │   │       ├── AppLayout.css
    │   │       ├── AppLayout.jsx
    │   │       ├── HomeLayout.css
    │   │       └── HomeLayout.jsx
    │   ├── features
    │   │   ├── ArtifactsPage
    │   │   │   └── ArtifactsPage.jsx
    │   │   ├── ArtifactDetailsPage
    │   │   │   └── ArtifactDetailsPage.jsx
    │   │   └── pagesAPI
    │   │       └── pages.api.js
    │   └── shared
    │       ├── components
    │           ├── Artifact (Artifact.css, Artifact.jsx, index.js)
    │           ├── ArtifactCard (ArtifactCard.css, ArtifactCard.jsx, index.js)
    │           ├── SortControls (SortControls.css, SortControls.jsx, index.js)
    │           ├── SearchBar (SearchBar.css, SearchBar.jsx, index.js)
    │           ├── Button
    │           └── Navbar
    ├── eslint.config.js
    ├── index.html
    ├── package.json
    ├── package-lock.json
    └── README.md
```

The frontend follows a modular, feature-based structure separating layout components, shared UI elements, and domain-specific features.

---

# How to Run the Backend API

## 1) Clone the Repository

```bash
git clone git@github.com:mnaeimaei/museum-catalog-api.git
```

or

```bash
git clone https://github.com/mnaeimaei/museum-catalog-api.git
```

---

## 2) Install .NET SDK

Download the .NET SDK from:

[https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)

Verify installation:

```bash
dotnet --version
```

---

## 3) Run the API

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

# How to Use the Web Application



The frontend application can run only if the backend application is running.  
After making sure that the backend application is running, open another terminal:


```bash
cd src
cd microchip.interview.web
npm run dev
```



Then the React-based app can be opened in a browser tab:


```text
http://localhost:5173/
```