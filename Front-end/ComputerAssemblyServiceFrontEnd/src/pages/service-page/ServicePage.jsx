import Page from "../Page/Page";
import "./ServicePage.css";
import { useEffect, useState } from "react";
import ServiceCard from "./ServiceCard/ServiceCard";
import SearchInput from "../../components/SearchInput/SearchInput";
import searchImg from "../../assets/SearchPng/search-50.png";
import servicePng from "../../assets/ServicePng/service-50.png";
import axios from "axios";

export default function ServicePage() {
  const [servicesData, setServicesData] = useState([]);
  const [searchQuery, setSearchQuery] = useState("");

  useEffect(() => {
    const fetchServices = async () => {
      try {
        const res = await axios.get("http://localhost:5153/api/Service");
        setServicesData(res.data);
      } catch (error) {
        console.error("Error fetching services:", error.message);
      }
    };

    fetchServices();
  }, []);

  const filteredServices = servicesData.filter((service) =>
    service.name.toLowerCase().includes(searchQuery.toLowerCase())
  );

  return (
    <Page className="service-page">
      <h1>Services</h1>
      <div className="line-br"></div>

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
            />
          ))
        ) : (
          <p>No services found...</p>
        )}
      </div>
    </Page>
  );
}
