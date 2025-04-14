import "./App.css";
import pcImage from "./assets/pc.png";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import MainPage from "./pages/main/MainPage";
import AboutUsPage from "./pages/about-us/AboutUsPage";
import ProductCard from "./components/ProductCard/ProductCard";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<MainPage />} />
        <Route path="/about-us-page" element={<AboutUsPage />} />
      </Routes>
    </Router>
  );
}

export default App;
