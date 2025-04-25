import "./CartProductCard.css";

export default function CartProductCard({ imgUrl, title, price, quantity }) {
  return (
    <article className="cart-product-card">
      <div className="product-info">
        <img src={imgUrl} alt={title} className="product-in-cart-img" />
        <p className="product-in-cart-title">{title}</p>
      </div>
      <div className="product-price-and-controls">
        <p className="product-in-cart-price">{price * quantity} UAH</p>
        <button className="button plus-product-quantity">+</button>
        <p className="product-in-cart-quantity">{quantity}</p>
        <button className="button minus-product-quantity">-</button>
      </div>
    </article>
  );
}
