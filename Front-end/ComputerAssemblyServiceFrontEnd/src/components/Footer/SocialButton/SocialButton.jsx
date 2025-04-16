import "./SocialButton.css";

export default function SocialButton({ icon, title, socialUrl }) {
  return (
    <a class="social-button" href={socialUrl} target="_blank">
      <img src={icon} alt={title} />
    </a>
  );
}
