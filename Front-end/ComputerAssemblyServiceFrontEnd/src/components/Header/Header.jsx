import "./Header.css";

import Navigation from "../Navigation/Navigation";

import logoPng from "../../assets/LogoPng/logo.png";
import userPng from "../../assets/UserPng/user-30.png";
import contactUsPng from "../../assets/ContactUsPng/contact-us-16.png";
import cartPng from "../../assets/CartPng/cart-24.png";

import { Link } from "react-router-dom";
import { useUser } from "../../contextes/UserContext";

export default function Header() {
  const { user, isAuthorized, logout } = useUser();

  return (
    <header>
      <div class="nav-continer">
        <img className="logo" src={logoPng} alt="BeePC logo" />
        <Navigation />
      </div>
      <div className="container">
        <Link to="/cart-page">
          <img src={cartPng} alt="Cart image" />
        </Link>
        <div className="vertical-line"></div>
        <Link to="/contact-page" className="contact-us link">
          <img src={contactUsPng} alt="Contact us image" />
          Contact us
        </Link>
        <div className="vertical-line"></div>
        {isAuthorized ? (
          <>
            <Link to="/user-page" className="user-account link">
              <img class="user-img" src={userPng} alt="User img logo" />
              {user?.firstName}
            </Link>
          </>
        ) : (
          <Link to="/auth-page" className="user-account link">
            <img class="user-img" src={userPng} alt="User img logo" />
            Sign in
          </Link>
        )}
      </div>
    </header>
  );
}
