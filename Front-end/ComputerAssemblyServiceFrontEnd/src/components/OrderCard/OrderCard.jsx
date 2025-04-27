import "./OrderCard.css";
import OrderItemCard from "../OrderItemCard/OrderItemCard";
import serviceImg from "../../assets/ServicePng/service-50.png";
import { OrderStatus } from "../../scripts/enums/order-status";

export default function OrderCard({ order }) {
  return (
    <div className="order-card">
      <h3>Order #{order.orderId}</h3>
      <div className="line-br" />
      <div className="order-container">
        {order.orderItems.length > 0 && (
          <div className="order-items">
            {order.orderItems.map((orderItem) => (
              <OrderItemCard
                key={orderItem.itemId}
                title={orderItem.product.prebuildPattern.prebuildName}
                imgUrl={orderItem.product.imgUrl}
                price={orderItem.product.prebuildPattern.basePrice}
                quantity={orderItem.quantity}
              />
            ))}
          </div>
        )}
        {order.orderServices.length > 0 && (
          <div className="order-services">
            {order.orderServices.map((orderService) => (
              <OrderItemCard
                key={orderService.orderServiceId}
                title={orderService.service.name}
                price={orderService.service.price}
                imgUrl={serviceImg}
                quantity={1}
                orderService={orderService}
              />
            ))}
          </div>
        )}
      </div>
      <div className="line-br" />
      <div className="add-order-info">
        <p>
          <strong>Total:</strong> {order.totalAmount} <strong>UAH</strong>
        </p>
        <p>Status: {order.status}</p>
      </div>
    </div>
  );
}
