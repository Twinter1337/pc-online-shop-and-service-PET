import { useState } from "react";
import "./ServiceCard.css";
import { useUser } from "../../../contextes/UserContext";
import { addOrderService } from "../../../scripts/services/order-service";
import { useNavigate } from "react-router-dom";

export default function ServiceCard({
  name,
  description,
  price,
  imageUrl,
  serviceId,
}) {
  const { user, isAuthorized } = useUser();
  const navigate = useNavigate();
  const [isAdded, setIsAdded] = useState(false);

  const handleAddOrderService = async () => {
    if (!isAuthorized) {
      localStorage.setItem("pendingCartService", JSON.stringify({ serviceId }));
      navigate("/auth-page");
      return;
    }

    await addOrderService(user, isAuthorized, serviceId);

    setIsAdded(true);

    setTimeout(() => {
      setIsAdded(false);
    }, 2000);
  };

  return (
    <div className="service-card">
      <img src={imageUrl} alt={name} className="service-card-image" />
      <div className="service-card-content">
        <h2 className="service-card-title">{name}</h2>
        <p className="service-card-description">{description}</p>
        <div className="service-card-footer">
          <p className="service-card-price">{price} UAH</p>
          <button
            className={`service-card-button button ${isAdded ? "added" : ""}`}
            onClick={async () => await handleAddOrderService()}
          >
            {isAdded ? "Purchased!" : "Order Now"}
          </button>
        </div>
      </div>
    </div>
  );
}
