// CartPage.jsx
import { useEffect, useState } from "react";
import "./CartPage.css";

import Page from "../Page/Page";
import CartProductCard from "../../components/CartProductCard/CartProductCard";
import PaymentForm from "./PaymentForm/PaymentForm";
import { useUser } from "../../contextes/UserContext";
import { getOrdersByUserId } from "../../scripts/services/order-service";
import { OrderStatus } from "../../scripts/enums/order-status";

export default function CartPage() {
  const { user } = useUser();
  const [newOrder, setNewOrder] = useState(null);

  useEffect(() => {
    const fetchNewOrder = async () => {
      if (!user) return;

      try {
        const orders = await getOrdersByUserId(user.userId);

        const orderWithStatusNew = orders.find(
          (order) => order.status === OrderStatus.New
        );

        setNewOrder(orderWithStatusNew);
      } catch (error) {
        console.error("Failed to fetch orders:", error);
      }
    };

    fetchNewOrder();
  }, [user, newOrder]);

  const handlePaymentSubmit = (paymentDetails) => {
    console.log("Payment submitted with details:", paymentDetails);
  };

  return (
    <Page className="cart-page">
      <section className="cart-page-container">
        <h1>Cart</h1>
        <div className="line-br"></div>
      </section>

      <section className="cart-items panel">
        {newOrder && newOrder.orderItems.length > 0 ? (
          newOrder.orderItems
            .filter((orderItem) => orderItem.quantity > 0)
            .map((orderItem) => (
              <CartProductCard
                key={orderItem.itemId}
                imgUrl={orderItem.product.imgUrl}
                title={orderItem.product.prebuildPattern.prebuildName}
                price={orderItem.product.prebuildPattern.basePrice}
                quantity={orderItem.quantity}
                itemId={orderItem.itemId}
              />
            ))
        ) : (
          <p className="empty-cart-p">Your cart is empty yet...</p>
        )}
      </section>

      {newOrder && newOrder.orderItems.length > 0 && (
        <section className="payment-section">
          <PaymentForm onSubmit={handlePaymentSubmit} user={user} />
        </section>
      )}
    </Page>
  );
}
