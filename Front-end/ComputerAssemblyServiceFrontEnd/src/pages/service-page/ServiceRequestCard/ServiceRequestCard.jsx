import "./ServiceRequestCard.css";
import CartProductCard from "../../../components/CartProductCard/CartProductCard";
import serviceImg from "../../../assets/ServicePng/service-50.png";
import { useEffect, useState } from "react";
import axios from "axios";
import { useUser } from "../../../contextes/UserContext";

export default function ServiceRequestCard({
  computerOnService,
  orderServices,
}) {
  const [services, setServices] = useState([]);
  const { user } = useUser();

  useEffect(() => {
    const fetchServices = async () => {
      try {
        const res = await axios.get("http://localhost:5153/api/Service");
        setServices(res.data);
      } catch (err) {
        console.error("Failed to fetch services:", err);
      }
    };

    fetchServices();
  }, []);

  const handleApplyToServiceWorker = async () => {
    try {
      const employeeRes = await axios.get(
        `http://localhost:5153/api/Employee/by-user-id/${user.userId}`
      );

      await axios.patch(
        `http://localhost:5153/api/ComputersOnService/${computerOnService.computerOnServiceId}`,
        {
          responsibleEmployeeId: employeeRes.data.employeeId,
        }
      );

      orderServices.forEach(async (orderService) => {
        await axios.patch(
          `http://localhost:5153/api/OrderService/${orderService.orderServiceId}`,
          {
            responsibleEmployeeId: employeeRes.data.employeeId,
          }
        );
      });

      onApply?.();
    } catch (err) {
      console.error(err);
    }
  };

  return (
    <article className="service-request-card">
      <div className="computer-info">
        <h3>Service Order #{computerOnService.computerOnServiceId}</h3>
        <button
          className="button apply-service-to-worker-button"
          onClick={async () => await handleApplyToServiceWorker()}
        >
          Apply to me
        </button>
      </div>

      <p>Needs services:</p>
      <div className="services-info">
        {orderServices.length > 0 ? (
          orderServices.map((orderService) => {
            const matchedService = services.find(
              (s) => s.serviceId === orderService.serviceId
            );

            if (!matchedService) {
              return null;
            }

            return (
              <CartProductCard
                key={orderService.orderServiceId}
                imgUrl={serviceImg}
                title={matchedService.name}
                price={matchedService.price}
                quantity={1}
                itemId={orderService.orderServiceId}
                isService={true}
                isRequest={true}
              />
            );
          })
        ) : (
          <p>No services needed</p>
        )}
      </div>
    </article>
  );
}
