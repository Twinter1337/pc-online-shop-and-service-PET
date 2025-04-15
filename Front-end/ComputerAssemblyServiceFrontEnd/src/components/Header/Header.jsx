import "./Header.css";

import Navigation from "../Navigation/Navigation";

import logoPng from "../../assets/LogoPng/logo.png";
import userPng from "../../assets/UserPng/user-30.png";

import { Link } from "react-router-dom";

export default function Header() {
  return (
    <header>
      <div class="nav-continer">
        <img class="logo" src={logoPng} alt="BeePC logo" />
        <Navigation />
      </div>
      <Link to="/user-page" class="user-account">
        <img class="user-img" src={userPng} alt="User img logo" />
        Sign in
      </Link>
    </header>
  );
}
