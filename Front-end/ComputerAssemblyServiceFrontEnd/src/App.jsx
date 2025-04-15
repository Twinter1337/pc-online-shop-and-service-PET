import "./App.css";

import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { AnimatePresence } from "framer-motion";

import HomePage from "./pages/home-page/HomePage";
import StorePage from "./pages/store-page/StorePage";
import ServicePage from "./pages/service-page/ServicePage";
import UserPage from "./pages/user-page/UserPage";
import ContactPage from "./pages/contact-page/ContactPage";

import Header from "./components/Header/Header";
import Footer from "./components/Footer/Footer";

export default function App() {
  return (
    <AnimatePresence mode="wait">
      <div class="layout">
        <Router>
          <Header />
          <main>
            <Routes>
              <Route path="/" element={<HomePage />} />
              <Route path="/store-page" element={<StorePage />} />
              <Route path="/service-page" element={<ServicePage />} />
              <Route path="/user-page" element={<UserPage />} />
              <Route path="/contact-page" element={<ContactPage />} />
            </Routes>
          </main>
          <Footer />
        </Router>
      </div>
    </AnimatePresence>
  );
}
