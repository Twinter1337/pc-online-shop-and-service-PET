import { useEffect, useState } from "react";
import arrowPng from "../../assets/UpArrowPngs/icons8-chevron-up-48.png";
import "./ScrollToTopButton.css";

export default function ScrollToTopButton() {
  const [isVisible, setIsVisible] = useState(false);

  useEffect(() => {
    const handleScroll = () => {
      setIsVisible(window.scrollY > 300); // показуємо після прокрутки вниз
    };
    window.addEventListener("scroll", handleScroll);
    return () => window.removeEventListener("scroll", handleScroll);
  }, []);

  const scrollToTop = () => {
    window.scrollTo({
      top: 0,
      behavior: "smooth",
    });
  };

  return (
    isVisible && (
      <button className="scroll-to-top" onClick={scrollToTop}>
        <img src={arrowPng} alt="" />
      </button>
    )
  );
}
