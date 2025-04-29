import "./NewServiceModal.css";
import { useState } from "react";
import axios from "axios";

export default function NewServiceModal({ closeModal }) {
  const [serviceName, setServiceName] = useState("");
  const [description, setDescription] = useState("");
  const [price, setPrice] = useState("");

  const handleCreateService = async () => {
    try {
      const enteredService = {
        name: serviceName,
        description: description,
        price: Number(price),
      };

      const newService = await axios.post(
        "http://localhost:5153/api/Service",
        enteredService
      );
      closeModal();
      console.log(newService);
    } catch (err) {
      console.error(err);
    }
  };

  return (
    <section className="new-service-modal">
      <h2>Create New Service</h2>
      <div className="line-br"></div>

      <div className="form-group">
        <label htmlFor="serviceName">Service Name</label>
        <input
          type="text"
          id="serviceName"
          placeholder="Enter service name"
          value={serviceName}
          onChange={(e) => setServiceName(e.target.value)}
        />
      </div>

      <div className="form-group">
        <label htmlFor="description">Description</label>
        <input
          type="text"
          id="description"
          placeholder="Enter service description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        />
      </div>

      <div className="form-group">
        <label htmlFor="price">Price (UAH)</label>
        <input
          type="number"
          id="price"
          placeholder="Enter price"
          value={price}
          onChange={(e) => setPrice(e.target.value)}
        />
      </div>

      <div className="line-br"></div>

      <button
        className="button create-service-button"
        onClick={handleCreateService}
      >
        Create Service
      </button>
    </section>
  );
}
