import "./SocialButton.css";

export default function SocialButton({ icon, title }) {
  return (
    <button class="social-button">
      <img src={icon} alt={title} />
    </button>
  );
}
