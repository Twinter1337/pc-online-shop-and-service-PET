import "./ManageOrdersModal.css";
import OrderCardForManaging from "./OrderCardForManaging/OrderCardForManaging";
import { useState } from "react";

export default function ManageOrdersModal({
  orders: initialOrders,
  onDeleteOrder,
  onUpdateOrderStatus,
}) {
  const [orders, setOrders] = useState(initialOrders);

  const handleDeleteOrder = (deletedOrderId) => {
    setOrders((prevOrders) =>
      prevOrders.filter((order) => order.orderId !== deletedOrderId)
    );
    onDeleteOrder(deletedOrderId);
  };

  const handleUpdateOrderStatus = (updatedOrderId, newStatus) => {
    setOrders((prevOrders) =>
      prevOrders.map((order) =>
        order.orderId === updatedOrderId
          ? { ...order, status: newStatus }
          : order
      )
    );
    onUpdateOrderStatus(updatedOrderId, newStatus);
  };

  return (
    <section className="orders-manage">
      {orders.length > 0 ? (
        orders.map((order) => (
          <OrderCardForManaging
            key={order.orderId}
            order={order}
            onDelete={handleDeleteOrder}
            onUpdateStatus={handleUpdateOrderStatus}
          />
        ))
      ) : (
        <p>No orders to manage.</p>
      )}
    </section>
  );
}
