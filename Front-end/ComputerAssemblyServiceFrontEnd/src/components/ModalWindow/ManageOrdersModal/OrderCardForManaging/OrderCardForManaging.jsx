import "./OrderCardForManaging.css";
import { useState } from "react";
import { OrderStatus } from "../../../../scripts/enums/order-status";
import axios from "axios";

export default function OrderCardForManaging({
  order,
  onDelete,
  onUpdateStatus,
}) {
  const [selectedStatus, setSelectedStatus] = useState(order.status);

  const handleStatusChange = async (e) => {
    const newStatus = e.target.value;
    setSelectedStatus(newStatus);

    try {
      await axios.patch(`http://localhost:5153/api/Order/${order.orderId}`, {
        status: newStatus,
      });

      if (onUpdateStatus) {
        onUpdateStatus(order.orderId, newStatus); // 🔥 тут
      }
    } catch (err) {
      console.error(err);
    }
  };

  const handleDeleteOrder = async () => {
    try {
      await axios.delete(`http://localhost:5153/api/Order/${order.orderId}`);
      if (onDelete) {
        onDelete(order.orderId);
      }
    } catch (err) {
      console.error(err);
    }
  };

  return (
    <section className="order-card-for-managing">
      <h3>Order #{order.orderId}</h3>

      <div className="order-managing-controls">
        <select
          value={selectedStatus}
          onChange={handleStatusChange}
          className="order-status-select"
        >
          {Object.entries(OrderStatus).map(([key, value]) => (
            <option key={key} value={value}>
              {key}
            </option>
          ))}
        </select>

        <button className="button" onClick={handleDeleteOrder}>
          -
        </button>
      </div>
    </section>
  );
}
