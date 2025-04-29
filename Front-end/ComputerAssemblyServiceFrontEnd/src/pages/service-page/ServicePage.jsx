import Page from "../Page/Page";
import "./ServicePage.css";
import { useEffect, useState } from "react";
import ServiceCard from "./ServiceCard/ServiceCard";
import SearchInput from "../../components/SearchInput/SearchInput";
import searchImg from "../../assets/SearchPng/search-50.png";
import servicePng from "../../assets/ServicePng/service-50.png";
import axios from "axios";
import { useUser } from "../../contextes/UserContext";
import { UserRole } from "../../scripts/enums/user-role";
import ServiceRequestCard from "./ServiceRequestCard/ServiceRequestCard";

export default function ServicePage() {
  const [servicesData, setServicesData] = useState([]);
  const [orderServices, setOrderServices] = useState([]);
  const [computersOnService, setComputersOnService] = useState([]);
  const [searchQuery, setSearchQuery] = useState("");
  const { user } = useUser();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const ordersRes = await axios.get(
          "http://localhost:5153/api/OrderService"
        );

        setOrderServices(ordersRes.data);

        const computersServiceRes = await axios.get(
          "http://localhost:5153/api/ComputersOnService"
        );

        setComputersOnService(computersServiceRes.data);
        const servicesRes = await axios.get(
          "http://localhost:5153/api/Service"
        );
        setServicesData(servicesRes.data);
      } catch (error) {
        console.error("Error fetching services:", error.message);
      }
    };

    fetchData();
  }, [user, computersOnService]);

  const filteredServices = servicesData.filter((service) =>
    service.name.toLowerCase().includes(searchQuery.toLowerCase())
  );

  return (
    <Page className="service-page">
      <h1>Services</h1>
      <div className="line-br"></div>

      {user?.role !== UserRole.ServiceWorker ? (
        <>
          <SearchInput
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
            searchImg={searchImg}
            placeholder="Search services..."
          />

          <div className="service-page-content">
            {filteredServices.length > 0 ? (
              filteredServices.map((service, index) => (
                <ServiceCard
                  key={index}
                  name={service.name}
                  description={service.description}
                  price={service.price}
                  imageUrl={servicePng}
                  serviceId={service.serviceId}
                />
              ))
            ) : (
              <p>No services found...</p>
            )}
          </div>
        </>
      ) : (
        <div className="service-requests">
          {computersOnService.filter(
            (computer) => computer.responsibleEmployeeId === null
          ).length > 0 ? (
            computersOnService
              .filter((computer) => computer.responsibleEmployeeId === null)
              .map((computer) => {
                const relatedOrderServices = orderServices.filter(
                  (orderService) =>
                    orderService.computerOnServiceId ===
                    computer.computerOnServiceId
                );

                if (relatedOrderServices.length === 0) return null;

                return (
                  <ServiceRequestCard
                    key={computer.computerOnServiceId}
                    computerOnService={computer}
                    orderServices={relatedOrderServices}
                  />
                );
              })
          ) : (
            <p>No service requests yet...</p>
          )}
        </div>
      )}
    </Page>
  );
}
