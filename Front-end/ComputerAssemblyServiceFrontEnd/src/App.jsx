import "./App.css";

import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

import HomePage from "./pages/home/HomePage";
import StorePage from "./pages/store-page/StorePage";
import ServicePage from "./pages/service-page/ServicePage";
import Header from "./components/Header/Header";

export default function App() {
  return (
    <Router>
      <Header />
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/store-page" element={<StorePage />} />
        <Route path="/service-page" element={<ServicePage />} />
      </Routes>
    </Router>
  );
}
