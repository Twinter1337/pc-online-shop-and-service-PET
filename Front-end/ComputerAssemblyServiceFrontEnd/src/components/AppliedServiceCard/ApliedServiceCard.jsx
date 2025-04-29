import { useEffect, useState } from "react";
import "./AppliedServiceCard.css";
import axios from "axios";

export default function AppliedServiceCard({
  computer,
  orderServices,
  onComplete,
}) {
  const [services, setServices] = useState([]);

  useEffect(() => {
    const fetchServices = async () => {
      try {
        const res = await axios.get("http://localhost:5153/api/Service");

        const filteredServices = res.data.filter((service) =>
          orderServices.some(
            (orderService) =>
              orderService.serviceId === service.serviceId &&
              orderService.computerOnServiceId === computer.computerOnServiceId
          )
        );

        setServices(filteredServices);
      } catch (err) {
        console.error("Failed to fetch services:", err);
      }
    };

    fetchServices();
  }, [orderServices, computer]);

  const handleServiceComplete = async () => {
    try {
      await axios.patch(
        `http://localhost:5153/api/ComputersOnService/${computer.computerOnServiceId}`,
        { status: "Done" }
      );

      onComplete(computer.computerOnServiceId);
    } catch (err) {
      console.error(err);
    }
  };

  return (
    <article className="applied-service-card">
      <h4>Service request #{computer.computerOnServiceId}</h4>
      <div className="needed-services-list">
        {services.length > 0 ? (
          services.map((service) => (
            <div key={service.serviceId} className="service">
              <label htmlFor={`service-${service.serviceId}`}>
                {service.name}
              </label>
              <input
                type="checkbox"
                id={`service-${service.serviceId}`}
                name="complete-service"
                className="checkbox-service"
              />
            </div>
          ))
        ) : (
          <p>No services assigned.</p>
        )}
      </div>
      <button
        className="button complete-service-request-button"
        onClick={async () => await handleServiceComplete()}
      >
        Complete
      </button>
    </article>
  );
}
