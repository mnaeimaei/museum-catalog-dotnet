// frontend/src/features/pagesAPI/pages.api.js
// 
const API_BASE_URL = "http://localhost:5029";

export async function fetchArtifacts(params = {}) {
  const url = new URL(`${API_BASE_URL}/artifacts`);

  Object.keys(params).forEach((key) => {
    if (params[key] !== null && params[key] !== "") {
      url.searchParams.append(key, params[key]);
    }
  });

  const response = await fetch(url);
  if (!response.ok) {
    throw new Error("Failed to fetch artifacts");
  }

  return response.json();
}

export async function fetchArtifactById(id) {
  const response = await fetch(`${API_BASE_URL}/artifacts/${id}`);

  if (!response.ok) {
    throw new Error(`Artifact with ID ${id} not found`);
  }

  return response.json();
}
