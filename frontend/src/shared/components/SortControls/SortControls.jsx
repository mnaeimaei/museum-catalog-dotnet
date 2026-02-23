// frontend/src/shared/components/SortControls/SortControls.js 
// 
import "./SortControls.css";

export default function SortControls({ sort, direction, onChange }) {
  return (
    <div className="sort-controls">
      <select value={sort} onChange={(e) => onChange("sort", e.target.value)}>
        <option value="">Sort by...</option>
        <option value="title">Title</option>
        <option value="artifact_type">Artifact Type</option>
        <option value="isbn">ISBN</option>
      </select>

      <select
        value={direction}
        onChange={(e) => onChange("direction", e.target.value)}
      >
        <option value="asc">Ascending</option>
        <option value="desc">Descending</option>
      </select>
    </div>
  );
}
