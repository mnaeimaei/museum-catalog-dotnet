// frontend/src/shared/components/ArtifactCard/ArtifactCard.js 

import "./ArtifactCard.css";

export default function ArtifactCard({ pub, onClick }) {
  return (
    <div className="artifact-card" onClick={onClick}>
      <h3>{pub.title}</h3>
      <p><strong>Type:</strong> {pub.artifactType}</p>
      <p><strong>ISBN:</strong> {pub.isbn}</p>
    </div>
  );
}
