// frontend/src/app/layout/AppLayout.jsx


import { Outlet, Link } from "react-router-dom";
import "./AppLayout.css";

export default function AppLayout() {
  return (
    <div className="app-layout">

      <header className="header">
        <h1>Artifacts Manager</h1>
        <nav>
          <Link to="/">Home</Link>
          <Link to="/artifacts">Artifacts</Link>
        </nav>
      </header>

      <main className="content">
        <Outlet />
      </main>

    </div>
  );
}
