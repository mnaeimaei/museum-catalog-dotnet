// frontend/src/features/ArtifactPage/ArtifactPage.jsx


import { useEffect, useState } from "react";
import { fetchArtifacts } from "../pagesAPI/pages.api";

import ArtifactCard from "../../shared/components/ArtifactCard";
import SearchBar from "../../shared/components/SearchBar";
import SortControls from "../../shared/components/SortControls";

export default function ArtifactsPage() {
  const [artifacts, setArtifacts] = useState([]);
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [title, setTitle] = useState("");
  const [isbn, setIsbn] = useState("");
  const [sort, setSort] = useState("");
  const [direction, setDirection] = useState("asc");
  const [total, setTotal] = useState(0);

const loadArtifacts = async () => {
  const data = await fetchArtifacts({
    page,
    pageSize,
    title,
    isbn,
    sort,
    direction
  });

  console.log("API RESPONSE:", data);  

  setArtifacts(data.data);
  setTotal(data.total);
};

  useEffect(() => {
    loadArtifacts();
  }, [page, pageSize, title, isbn, sort, direction]);

  const handleSearchChange = (field, value) => {
    if (field === "title") setTitle(value);
    if (field === "isbn") setIsbn(value);
    setPage(1);
  };

  const handleSortChange = (field, value) => {
    if (field === "sort") setSort(value);
    if (field === "direction") setDirection(value);
  };

  return (
    <div>
      <h2>Artifacts</h2>

      <SearchBar title={title} isbn={isbn} onChange={handleSearchChange} />
      <SortControls sort={sort} direction={direction} onChange={handleSortChange} />

      <div>
        {artifacts.map((pub) => (
          <ArtifactCard
            key={pub.id}
            pub={pub}
            onClick={() => (window.location.href = `/artifacts/${pub.id}`)}
          />
        ))}
      </div>

      {/* Paging */}
      <div style={{ marginTop: "20px" }}>
        <button disabled={page === 1} onClick={() => setPage(page - 1)}>
          Previous
        </button>
        <span style={{ margin: "0 15px" }}>Page {page}</span>
        <button
          disabled={page * pageSize >= total}
          onClick={() => setPage(page + 1)}
        >
          Next
        </button>

        <select
          value={pageSize}
          onChange={(e) => setPageSize(Number(e.target.value))}
          style={{ marginLeft: "15px" }}
        >
          <option value={5}>5</option>
          <option value={10}>10</option>
          <option value={25}>25</option>
        </select>
      </div>
    </div>
  );
}
