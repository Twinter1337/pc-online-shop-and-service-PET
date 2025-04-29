import { useEffect, useState } from "react";
import "./UserPage.css";
import Page from "../Page/Page";
import { useUser } from "../../contextes/UserContext";
import { UserRole } from "../../scripts/enums/user-role";
import { Link } from "react-router-dom";
import OrderCard from "../../components/OrderCard/OrderCard";
import InfoRow from "../../components/InfoRow/InfoRow";
import { getOrdersByUserId } from "../../scripts/services/order-service";
import { OrderStatus } from "../../scripts/enums/order-status";
import axios from "axios";
import AppliedServiceCard from "../../components/AppliedServiceCard/ApliedServiceCard";
import { ServiceStatus } from "../../scripts/enums/service-status";
import ModalWindow from "../../components/ModalWindow/ModalWindow";
import NewPrebuildPatternModal from "../../components/ModalWindow/NewPrebuildPatternModal/NewPrebuildPatternModal";
import NewServiceModal from "../../components/ModalWindow/NewServiceModal/NewServiceModal";
import ManageOrdersModal from "../../components/ModalWindow/ManageOrdersModal/ManageOrdersModal";
import GenerateReportModal from "../../components/ModalWindow/GenerateReportModal/GenerateReportModal";
import NewComponentModal from "../../components/ModalWindow/NewComponentModal/NewComponentModal";

export default function UserPage() {
  const { user, isAuthorized, logout, employee } = useUser();
  const [orders, setOrders] = useState([]);
  const [appliedServices, setAppliedServices] = useState([]);
  const [appliedComputersOnService, setAppliedComputersOnService] = useState(
    []
  );
  const [isModalOpen, setIsModalOpen] = useState(0);

  useEffect(() => {
    const fetchOrders = async () => {
      if (!user) return;
      try {
        const fetchedOrders = await getOrdersByUserId(user.userId);

        const filteredOrders = fetchedOrders.filter(
          (order) => order.status !== OrderStatus.New
        );

        setOrders(filteredOrders);
      } catch (error) {
        console.error("Failed to fetch orders:", error);
      }
    };

    const fetchServiceRequests = async () => {
      if (employee == null || employee.employeeId == null) {
        return;
      }
      try {
        const fetchedServices = await axios.get(
          "http://localhost:5153/api/OrderService"
        );

        const fetchedComputersOnService = await axios.get(
          "http://localhost:5153/api/ComputersOnService"
        );

        const filteredComputersOnServices =
          fetchedComputersOnService.data.filter(
            (computer) =>
              computer.responsibleEmployeeId === employee.employeeId &&
              computer.status !== ServiceStatus.Done
          );

        const filteredServices = fetchedServices.data.filter(
          (service) => service.responsibleEmployeeId === employee.employeeId
        );

        setAppliedComputersOnService(filteredComputersOnServices);
        setAppliedServices(filteredServices);
      } catch (err) {
        console.error(err);
      }
    };

    if (user?.role === UserRole.ServiceWorker) fetchServiceRequests();
    fetchOrders();
  }, [user, employee]);

  if (!isAuthorized) {
    return null;
  }

  return (
    <Page className="user-page">
      {/* MODALS */}
      <ModalWindow
        isOpen={isModalOpen === 1}
        onClose={() => setIsModalOpen(false)}
      >
        <NewPrebuildPatternModal closeModal={() => setIsModalOpen(false)} />
      </ModalWindow>
      <ModalWindow
        isOpen={isModalOpen === 2}
        onClose={() => setIsModalOpen(false)}
      >
        <NewComponentModal />
      </ModalWindow>
      <ModalWindow
        isOpen={isModalOpen === 3}
        onClose={() => setIsModalOpen(false)}
      >
        <NewServiceModal closeModal={() => setIsModalOpen(false)} />
      </ModalWindow>
      <ModalWindow
        isOpen={isModalOpen === 5}
        onClose={() => setIsModalOpen(false)}
      >
        <ManageOrdersModal
          orders={orders}
          onDeleteOrder={(deletedOrderId) =>
            setOrders((prev) =>
              prev.filter((order) => order.orderId !== deletedOrderId)
            )
          }
          onUpdateOrderStatus={(updatedOrderId, newStatus) =>
            setOrders((prev) =>
              prev.map((order) =>
                order.orderId === updatedOrderId
                  ? { ...order, status: newStatus }
                  : order
              )
            )
          }
        />
      </ModalWindow>
      <ModalWindow
        isOpen={isModalOpen === 6}
        onClose={() => setIsModalOpen(false)}
      >
        <GenerateReportModal />
      </ModalWindow>
      {/* MODALS */}

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
            <button className="button" onClick={() => setIsModalOpen(1)}>
              Add new prebuild computer
            </button>
            <button className="button" onClick={() => setIsModalOpen(2)}>
              Add new component
            </button>
            <button className="button" onClick={() => setIsModalOpen(3)}>
              Add new service
            </button>
            <button className="button" onClick={() => setIsModalOpen(5)}>
              Manage orders
            </button>
            <button className="button" onClick={() => setIsModalOpen(6)}>
              Generate report
            </button>
          </div>
        </section>
      )}
      {user.role === UserRole.ServiceWorker && (
        <section className="employee-panel panel">
          <h2>Service Worker Panel:</h2>
          <div className="line-br" />
          <div className="employee-panel__content">
            {appliedComputersOnService.length > 0 ? (
              appliedComputersOnService.map((computer) => (
                <AppliedServiceCard
                  key={computer.computerOnServiceId}
                  computer={computer}
                  orderServices={appliedServices}
                  onComplete={(completedId) => {
                    setAppliedComputersOnService((prev) =>
                      prev.filter((c) => c.computerOnServiceId !== completedId)
                    );
                  }}
                />
              ))
            ) : (
              <p>No applied services yet...</p>
            )}
          </div>
        </section>
      )}
    </Page>
  );
}
