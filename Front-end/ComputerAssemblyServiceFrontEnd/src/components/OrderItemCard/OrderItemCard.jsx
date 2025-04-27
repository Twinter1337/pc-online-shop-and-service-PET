import "./OrderItemCard.css";

export default function OrderItemCard({ title, imgUrl, price, quantity }) {
  return (
    <article className="order-item-card">
      <div className="order-item__img">
        <img src={imgUrl} alt={title} />
      </div>
      <div className="order-item__content">
        <h3 className="order-item__title">{title}</h3>
        <div className="order-item__details">
          <p className="order-item__price">
            <strong>Price:</strong> {price.toFixed(2)} UAH
          </p>
          <p className="order-item__quantity">
            <strong>Quantity:</strong> {quantity}
          </p>
        </div>
      </div>
    </article>
  );
}
