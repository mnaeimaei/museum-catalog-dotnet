// frontend/src/app/layout/HomeLayout.jsx


import { Outlet, Link } from "react-router-dom";
import "./HomeLayout.css";

export default function HomeLayout() {
  return (
    <div className="home-layout">
      <div className="home-content">
        <Outlet />

        <Link className="enter-btn" to="/artifacts">
          Enter Artifacts
        </Link>
      </div>
    </div>
  );
}
