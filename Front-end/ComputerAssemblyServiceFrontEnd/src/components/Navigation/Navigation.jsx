import "./Navigation.css";

import NavigationLink from "./NavigationLink/NavigationLink";

import homePageImg from "../../assets/HomePng/home-24.png";
import storePageImg from "../../assets/StorePng/pc-24.png";
import servicePageImg from "../../assets/ServicePng/computer-support-24.png";

export default function Navigation() {
  return (
    <nav>
      {/* <NavigationLink image={homePageImg} title="Home" to="/" /> */}
      <NavigationLink image={storePageImg} title="Store" to="/" />
      <NavigationLink
        image={servicePageImg}
        title="Service"
        to="/service-page"
      />
    </nav>
  );
}
