import { Link } from "react-router-dom";
import "./NavigationLink.css";

export default function NavigationLink({ image, title, to }) {
  return (
    <Link className="nav-link" to={to}>
      <div className="nav-content">
        <img src={image} alt={title} />
        <p>{title}</p>
      </div>
    </Link>
  );
}
