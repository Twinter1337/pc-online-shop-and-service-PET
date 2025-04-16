import "./Footer.css";

import SocialButton from "./SocialButton/SocialButton";

import facebookIcon from "../../assets/MediaPngs/facebook-logo-30.png";
import instagramIcon from "../../assets/MediaPngs/instagram-logo-30.png";
import twitterIcon from "../../assets/MediaPngs/x-30.png";
import youtubeIcon from "../../assets/MediaPngs/youtube-logo-30.png";
import logoPng from "../../assets/LogoPng/logo.png";

export default function Footer() {
  return (
    <footer class="social-media">
      <section class="social-media-buttons-section">
        <SocialButton icon={facebookIcon} title="Facebook logo image" />
        <SocialButton icon={twitterIcon} title="X logo image" />
        <SocialButton icon={instagramIcon} title="Instagram logo image" />
        <SocialButton
          icon={youtubeIcon}
          title="Youtube logo image"
          socialUrl="https://www.youtube.com/watch?v=xvFZjo5PgG0&pp=ygUJcmljayByb2xs"
        />
      </section>
      <img class="logo" src={logoPng} alt="BeePC logo image" />
      <p>&copy; 2025 BeePC Company. All Rights Reserved.</p>
    </footer>
  );
}
