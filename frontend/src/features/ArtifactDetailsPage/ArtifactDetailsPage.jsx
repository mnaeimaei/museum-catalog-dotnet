
// frontend/src/features/ArtifactDetailsPage/ArtifactDetailsPage.jsx

import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

import { fetchArtifactById } from "../pagesAPI/pages.api";
import Artifact from "../../shared/components/Artifact";

export default function ArtifactDetailsPage() {
  const { id } = useParams();
  const [artifact, setArtifact] = useState(null);

  useEffect(() => {
    async function load() {
      try {
        const data = await fetchArtifactById(id);
        setArtifact(data);
      } catch (err) {
        console.error(err);
        setArtifact(null);
      }
    }

    load();
  }, [id]);

  return (
    <div>
      <h2>Artifact Details</h2>
      <Artifact artifact={artifact} />
    </div>
  );
}
