import "./Footer.css";

import SocialButton from "./SocialButton/SocialButton";

import facebookIcon from "../../assets/MediaPngs/facebook-logo-30.png";
import instagramIcon from "../../assets/MediaPngs/instagram-logo-30.png";
import twitterIcon from "../../assets/MediaPngs/x-30.png";
import youtubeIcon from "../../assets/MediaPngs/youtube-logo-30.png";

export default function Footer() {
  return (
    <footer>
      <div class="footer-nav">
        Lorem, ipsum dolor sit amet consectetur adipisicing elit. Repellendus,
        deleniti harum? Nesciunt placeat voluptates beatae, odio saepe doloribus
        pariatur ad, velit vel a, aperiam magni quidem eius sapiente eum eos?
        Lorem, ipsum dolor sit amet consectetur adipisicing elit. Repellendus,
        deleniti harum? Nesciunt placeat voluptates beatae, odio saepe doloribus
        pariatur ad, velit vel a, aperiam magni quidem eius sapiente eum eos?
        Lorem, ipsum dolor sit amet consectetur adipisicing elit. Repellendus,
        deleniti harum? Nesciunt placeat voluptates beatae, odio saepe doloribus
        pariatur ad, velit vel a, aperiam magni quidem eius sapiente eum eos?
        Lorem, ipsum dolor sit amet consectetur adipisicing elit. Repellendus,
        deleniti harum? Nesciunt placeat voluptates beatae, odio saepe doloribus
        pariatur ad, velit vel a, aperiam magni quidem eius sapiente eum eos?
        Lorem, ipsum dolor sit amet consectetur adipisicing elit. Repellendus,
        deleniti harum? Nesciunt placeat voluptates beatae, odio saepe doloribus
        pariatur ad, velit vel a, aperiam magni quidem eius sapiente eum eos?
        Lorem, ipsum dolor sit amet consectetur adipisicing elit. Repellendus,
        deleniti harum? Nesciunt placeat voluptates beatae, odio saepe doloribus
        pariatur ad, velit vel a, aperiam magni quidem eius sapiente eum eos?
      </div>
      <div class="social-media">
        <section class="social-media-buttons-section">
          <SocialButton icon={facebookIcon} title="Facebook logo image" />
          <SocialButton icon={twitterIcon} title="X logo image" />
          <SocialButton icon={instagramIcon} title="Instagram logo image" />
          <SocialButton icon={youtubeIcon} title="Youtube logo image" />
        </section>
        <p>&copy; 2025 BeePC Company. All Rights Reserved.</p>
      </div>
    </footer>
  );
}
