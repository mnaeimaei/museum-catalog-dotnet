// frontend/src/app/routes.jsx

import { Routes, Route } from "react-router-dom";

import AppLayout from "./layout/AppLayout";
import HomeLayout from "./layout/HomeLayout";

import ArtifactsPage from "@/features/ArtifactsPage/ArtifactsPage";
import ArtifactDetailsPage from "@/features/ArtifactDetailsPage/ArtifactDetailsPage";

export default function AppRoutes() {
  return (
    <Routes>
      <Route element={<HomeLayout />}>
        <Route path="/" element={<h2>Welcome to Artifacts Manager</h2>} />
      </Route>

      <Route element={<AppLayout />}>
        <Route path="/artifacts" element={<ArtifactsPage />} />
        <Route path="/artifacts/:id" element={<ArtifactDetailsPage />} />
      </Route>
    </Routes>
  );
}
