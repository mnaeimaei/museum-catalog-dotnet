// frontend/src/shared/components/Artifact/Artifact.js 

import "./Artifact.css";

export default function Artifact({ artifact }) {
  if (!artifact) return <p>No artifact found.</p>;

  return (
    <div className="artifact-details">
      <h2>{artifact.title}</h2>

      <p><strong>Type:</strong> {artifact.artifactType}</p>
      <p><strong>ISBN:</strong> {artifact.isbn}</p>
      <p><strong>Description:</strong> {artifact.description}</p>

      <h3>Versions</h3>
      <ul>
        {artifact.versions.map(v => (
          <li key={v.id}>
            <strong>{v.version}</strong> - {v.coverTitle} ({v.language})
          </li>
        ))}
      </ul>
    </div>
  );
}
