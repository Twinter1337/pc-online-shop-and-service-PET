//Styles
import "./ProductCharacteristic.css";

export default function ProductCharacteristic({
  imageUrl,
  componentName,
  componentModel,
}) {
  return (
    <div className="characteristic">
      <img src={imageUrl} alt={componentName} />
      <span>{componentName}</span> <p>{componentModel}</p>
    </div>
  );
}
