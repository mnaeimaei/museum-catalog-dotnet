// frontend/src/shared/components/SearchBar/SearchBar.js 
// 
import "./SearchBar.css";

export default function SearchBar({ title, isbn, onChange }) {
  return (
    <div className="search-bar">
      <input
        placeholder="Search by title..."
        value={title}
        onChange={(e) => onChange("title", e.target.value)}
      />

      <input
        placeholder="Search by ISBN..."
        value={isbn}
        onChange={(e) => onChange("isbn", e.target.value)}
      />
    </div>
  );
}
