import { useEffect, useState } from "react";
import "./UserPage.css";
import Page from "../Page/Page";
import { useUser } from "../../contextes/UserContext";
import { UserRole } from "../../scripts/enums/user-role";
import { Link } from "react-router-dom";
import OrderCard from "../../components/OrderCard/OrderCard";
import InfoRow from "../../components/InfoRow/InfoRow";
import { getOrdersByUserId } from "../../scripts/services/order-service";
import { OrderStatus } from "../../scripts/enums/order-status"; // Імпортуємо OrderStatus

export default function UserPage() {
  const { user, isAuthorized, logout } = useUser();
  const [orders, setOrders] = useState([]);

  useEffect(() => {
    const fetchOrders = async () => {
      if (!user) return;
      try {
        const fetchedOrders = await getOrdersByUserId(user.userId);

        const filteredOrders = fetchedOrders.filter(
          (order) => order.status !== OrderStatus.New
        );

        console.log(filteredOrders);

        setOrders(filteredOrders);
      } catch (error) {
        console.error("Failed to fetch orders:", error);
      }
    };

    fetchOrders();
  }, [user]);

  if (!isAuthorized) {
    return null;
  }

  return (
    <Page className="user-page">
      <section className="user-page-greeting">
        <h1>Welcome, {user.firstName}!</h1>
        <div className="line-br" />
      </section>

      <section className="user-page-info panel">
        <h2>Your information:</h2>
        <div className="line-br" />
        <div className="user-page-info__content">
          <InfoRow label="First Name" value={user.firstName} />
          <InfoRow label="Last Name" value={user.lastName} />
          <InfoRow label="Email" value={user.email} />
          <InfoRow label="Phone" value={user.phone || "N/A"} />
        </div>
        <div className="line-br" />
        <Link to="/auth-page">
          <button onClick={logout} className="button logout-button">
            Log out
          </button>
        </Link>
      </section>

      <section className="user-orders panel">
        <h2>Your orders:</h2>
        <div className="line-br" />
        <div className="user-orders__content">
          {orders.length > 0 ? (
            orders.map((order) => (
              <OrderCard key={order.orderId} order={order} />
            ))
          ) : (
            <p>You have no orders yet!</p>
          )}
        </div>
      </section>

      {user.role === UserRole.Manager && (
        <section className="employee-panel panel">
          <h2>Manager Panel:</h2>
          <div className="line-br" />
          <div className="employee-panel__content">
            <button className="button">Add new prebuild computer</button>
            <button className="button">Add new service</button>
            <button className="button">Manage users</button>
            <button className="button">Manage orders</button>
          </div>
        </section>
      )}
      {user.role === UserRole.ServiceWorker && (
        <section className="employee-panel panel">
          <h2>Service Worker Panel:</h2>
          <div className="line-br" />
          <div className="employee-panel__content"></div>
        </section>
      )}
    </Page>
  );
}
