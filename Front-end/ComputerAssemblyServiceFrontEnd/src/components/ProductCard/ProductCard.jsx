import { useEffect, useState } from "react";
import "./ProductCard.css";

//Image imports
import cpuImg from "../../assets/ProductCardSvgs/cpu-part.svg";
import gpuImg from "../../assets/ProductCardSvgs/gpu-part.svg";
import ramImg from "../../assets/ProductCardSvgs/ram-part.svg";
import hddImg from "../../assets/ProductCardSvgs/hdd-part.svg";

//Component imports
import ProductCharacteristic from "./ProductCharacteristic/ProductCharacteristic";
import { addOrderItem } from "../../scripts/services/order-service";
import { useUser } from "../../contextes/UserContext";
import { useNavigate } from "react-router-dom";

export default function ProductCard({
  title,
  imageUrl,
  processor,
  videoCard,
  ram,
  storage,
  price,
  productId,
}) {
  const { user, isAuthorized } = useUser();
  const navigate = useNavigate();
  const [isAdded, setIsAdded] = useState(false);

  const handleAddToCart = async () => {
    if (!isAuthorized) {
      localStorage.setItem(
        "pendingCartItem",
        JSON.stringify({ productId, price })
      );

      navigate("/auth-page");
      return;
    }

    await addOrderItem(user, isAuthorized, productId, price);

    setIsAdded(true);

    setTimeout(() => {
      setIsAdded(false);
    }, 2000);
  };

  const characteristics = [
    { image: cpuImg, name: "CPU", model: processor },
    { image: gpuImg, name: "GPU", model: videoCard },
    { image: ramImg, name: "RAM", model: ram },
    { image: hddImg, name: "HDD/SSD", model: storage },
  ];

  return (
    <article className="product-card">
      <div className="product-img-container">
        <img src={imageUrl} alt={title} className="product-image" />
      </div>
      <h2 className="product-title">{title}</h2>
      <div className="line"></div>
      <div className="product-specs">
        {characteristics.map((char, index) => (
          <ProductCharacteristic
            key={index}
            imageUrl={char.image}
            componentName={char.name}
            componentModel={char.model}
          />
        ))}
      </div>
      <div className="line"></div>
      <div className="product-footer">
        <div className="product-price">{price} UAH</div>
        <div className="product-actions">
          <button
            className={isAdded ? "added" : ""}
            onClick={async () => await handleAddToCart()}
          >
            {isAdded ? "Purchased!" : "Purchase"}
          </button>
        </div>
      </div>
    </article>
  );
}
