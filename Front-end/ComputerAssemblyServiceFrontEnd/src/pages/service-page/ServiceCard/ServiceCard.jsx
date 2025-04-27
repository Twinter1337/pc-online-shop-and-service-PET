import "./ServiceCard.css";
import { Link } from "react-router-dom";

export default function ServiceCard({ name, description, price, imageUrl }) {
  return (
    <div className="service-card">
      <img src={imageUrl} alt={name} className="service-card-image" />
      <div className="service-card-content">
        <h2 className="service-card-title">{name}</h2>
        <p className="service-card-description">{description}</p>
        <div className="service-card-footer">
          <p className="service-card-price">{price} UAH</p>
          <button className="service-card-button button">Order Now</button>
        </div>
      </div>
    </div>
  );
}
