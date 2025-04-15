import "./Header.css";

import Navigation from "../Navigation/Navigation";

import logoPng from "../../assets/LogoPng/logo.png";
import userPng from "../../assets/UserPng/user-30.png";
import contactUsPng from "../../assets/ContactUsPng/contact-us-16.png";

import { Link } from "react-router-dom";

export default function Header() {
  return (
    <header>
      <div class="nav-continer">
        <img class="logo" src={logoPng} alt="BeePC logo" />
        <Navigation />
      </div>
      <div class="container">
        <Link to="/contact-page" class="contact-us link">
          <img src={contactUsPng} alt="Contact us image" />
          Contact us
        </Link>
        <div className="vertical-line"></div>
        <Link to="/user-page" class="user-account link">
          <img class="user-img" src={userPng} alt="User img logo" />
          Sign in
        </Link>
      </div>
    </header>
  );
}
