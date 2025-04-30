import { useState } from "react";
import "./CartProductCard.css";
import {
  updateOrderItemQuantity,
  deleteOrderItem,
  deleteOrderService,
} from "../../scripts/services/order-service";
import bucketImg from "../../assets/BucketPng/delete-48.png";

export default function CartProductCard({
  imgUrl,
  title,
  price,
  quantity: initialQuantity,
  itemId,
  isService,
  isRequest,
  onChange,
}) {
  const [quantity, setQuantity] = useState(initialQuantity);

  const handleIncrease = async () => {
    const newQuantity = quantity + 1;
    setQuantity(newQuantity);

    try {
      await updateOrderItemQuantity(itemId, newQuantity);
      onChange?.();
    } catch (error) {
      console.error("Failed to update quantity:", error);
    }
  };

  const handleDecrease = async () => {
    const newQuantity = quantity - 1;

    if (newQuantity > 0) {
      setQuantity(newQuantity);

      try {
        await updateOrderItemQuantity(itemId, newQuantity);
        onChange?.();
      } catch (error) {
        console.error("Failed to update quantity:", error);
      }
    } else {
      try {
        await deleteOrderItem(itemId);
        onChange?.();
      } catch (error) {
        console.error("Failed to delete item:", error);
      }
    }
  };

  const handleDeleteOrderService = async () => {
    try {
      await deleteOrderService(itemId);
      onChange?.();
    } catch (err) {
      console.error(err);
    }
  };

  return (
    <article className="cart-product-card">
      <div className="product-info">
        <img src={imgUrl} alt={title} className="product-in-cart-img" />
        <p className="product-in-cart-title">{title}</p>
      </div>
      <div className="product-price-and-controls">
        <p className="product-in-cart-price">{price} UAH</p>
        {isService === false ? (
          <>
            <button
              className="button minus-product-quantity"
              onClick={handleDecrease}
            >
              <strong>-</strong>
            </button>
            <p className="product-in-cart-quantity">{quantity}</p>
            <button
              className="button plus-product-quantity"
              onClick={handleIncrease}
            >
              <strong>+</strong>
            </button>
          </>
        ) : (
          !isRequest && (
            <button
              className="button"
              onClick={async () => await handleDeleteOrderService()}
            >
              <img
                className="bucket-img-delete-button"
                src={bucketImg}
                alt="bucket image"
              />
            </button>
          )
        )}
      </div>
    </article>
  );
}
