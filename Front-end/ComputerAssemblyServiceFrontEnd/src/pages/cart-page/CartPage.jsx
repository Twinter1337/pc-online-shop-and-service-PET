// CartPage.jsx
import { useEffect, useState } from "react";
import axios from "axios";
import "./CartPage.css";

import Page from "../Page/Page";
import CartProductCard from "../../components/CartProductCard/CartProductCard";
import PaymentForm from "./PaymentForm/PaymentForm";
import { useUser } from "../../contextes/UserContext";
import { getOrdersByUserId } from "../../scripts/services/order-service";
import { OrderStatus } from "../../scripts/enums/order-status";
import servicePng from "../../assets/ServicePng/service-50.png";

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
  }, [user]);

  const refreshOrder = async () => {
    try {
      const orders = await getOrdersByUserId(user.userId);
      const orderWithStatusNew = orders.find(
        (order) => order.status === OrderStatus.New
      );
      setNewOrder(orderWithStatusNew);
    } catch (error) {
      console.error("Failed to refresh orders:", error);
    }
  };

  const handlePaymentSubmit = async () => {
    const newPayment = {
      amount: newOrder.totalAmount,
      orderId: newOrder.orderId,
    };
    try {
      const res = await axios.post(
        "http://localhost:5153/api/Payment",
        newPayment
      );

      console.log(res);
      if (res.status === 201) {
        console.log("hello");
        await axios.patch(
          `http://localhost:5153/api/Order/${newOrder.orderId}`,
          { status: OrderStatus.Paid }
        );

        const orderServices = await axios.get(
          `http://localhost:5153/api/OrderService/by-order-id/${newOrder.orderId}`
        );

        if (orderServices.data.length > 0) {
          const newComputerOnService = await axios.post(
            "http://localhost:5153/api/ComputersOnService",
            {
              userId: user.userId,
              problemDescription: "needs service",
            }
          );

          for (let i = 0; i < orderServices.data.length; i++) {
            await axios.patch(
              `http://localhost:5153/api/OrderService/${orderServices.data[i].orderServiceId}`,
              {
                computerOnServiceId:
                  newComputerOnService.data.computerOnServiceId,
              }
            );
          }
        }
      }

      await refreshOrder();
    } catch (err) {
      console.error(err);
    }
  };

  return (
    <Page className="cart-page">
      <section className="cart-page-container">
        <h1>Cart</h1>
        <div className="line-br"></div>
      </section>

      <section
        className={`cart-items panel ${
          newOrder &&
          (newOrder.orderItems.length > 0 || newOrder.orderServices.length > 0)
            ? "narrow"
            : "wide"
        }`}
      >
        {newOrder &&
        (newOrder.orderItems.length > 0 ||
          newOrder.orderServices.length > 0) ? (
          <>
            {newOrder.orderItems
              .filter((orderItem) => orderItem.quantity > 0)
              .map((orderItem) => (
                <CartProductCard
                  key={`product-${orderItem.itemId}`}
                  imgUrl={orderItem.product.imgUrl}
                  title={orderItem.product.prebuildPattern.prebuildName}
                  price={orderItem.product.prebuildPattern.basePrice}
                  quantity={orderItem.quantity}
                  itemId={orderItem.itemId}
                  isService={false}
                  onChange={refreshOrder}
                />
              ))}

            {newOrder.orderServices.map((orderService) => (
              <CartProductCard
                key={`service-${orderService.orderServiceId}`}
                imgUrl={servicePng}
                title={orderService.service.name}
                price={orderService.service.price}
                itemId={orderService.orderServiceId}
                isService={true}
                onChange={refreshOrder}
              />
            ))}
          </>
        ) : (
          <p className="empty-cart-p">Your cart is empty yet...</p>
        )}
      </section>

      {newOrder &&
        (newOrder.orderItems.length > 0 ||
          newOrder.orderServices.length > 0) && (
          <section className="payment-section">
            <PaymentForm onSubmit={handlePaymentSubmit} user={user} />
          </section>
        )}
    </Page>
  );
}
