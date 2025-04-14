import "./NavigationLink.css";

import { Link } from "react-router-dom";

export default function NavigationLink({ image, title, to }) {
  return (
    <Link class="nav-link" to={to}>
      <img src={image} alt={title} />
      <p>{title}</p>
    </Link>
  );
}
