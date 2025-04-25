//Styles
import "./ProductCard.css";

//Image imports
import cpuImg from "../../assets/ProductCardSvgs/cpu-part.svg";
import gpuImg from "../../assets/ProductCardSvgs/gpu-part.svg";
import ramImg from "../../assets/ProductCardSvgs/ram-part.svg";
import hddImg from "../../assets/ProductCardSvgs/hdd-part.svg";

//Component imports
import ProductCharacteristic from "./ProductCharacteristic/ProductCharacteristic";

export default function ProductCard({
  title,
  imageUrl,
  processor,
  videoCard,
  memoryType,
  ram,
  storage,
  price,
}) {
  const characteristics = [
    { image: cpuImg, name: "CPU", model: processor },
    { image: gpuImg, name: "GPU", model: videoCard },
    { image: ramImg, name: "RAM", model: ram },
    { image: hddImg, name: "HDD/SSD", model: storage },
  ];

  return (
    <article className="product-card">
      <img src={imageUrl} alt={title} className="product-image" />

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
          <button>Purchase</button>
        </div>
      </div>
    </article>
  );
}
