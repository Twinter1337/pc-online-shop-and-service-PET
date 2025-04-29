import { useState } from "react";
import "./NewComponentModal.css";
import { ComponentType } from "../../../scripts/enums/component-type";
import axios from "axios";

export default function NewComponentModal() {
  const [componentData, setComponentData] = useState({
    manufacturer: "",
    model: "",
    price: 0,
    category: "",
    quantityOnStock: 0,
    specs: {},
  });

  const [error, setError] = useState("");

  const handleChange = (e) => {
    const { name, value } = e.target;
    setComponentData((prev) => ({
      ...prev,
      [name]:
        name === "price" || name === "quantityOnStock" ? Number(value) : value,
    }));
  };
  const handleSpecChange = (e) => {
    const { name, value } = e.target;
    setComponentData((prev) => ({
      ...prev,
      specs: { ...prev.specs, [name]: value },
    }));
  };

  const validateFields = () => {
    const { manufacturer, model, price, category, quantityOnStock, specs } =
      componentData;

    if (
      !manufacturer.trim() ||
      !model.trim() ||
      !price ||
      !category ||
      !quantityOnStock
    ) {
      return "Please fill in all base fields.";
    }

    if (price <= 0 || quantityOnStock < 0) {
      return "Price must be greater than 0 and stock cannot be negative.";
    }

    const requiredSpecs = Object.keys(specs);
    if (
      requiredSpecs.length < 3 ||
      Object.values(specs).some((val) => val === "")
    ) {
      return "Please fill in all specification fields.";
    }

    // Перевірка на від’ємні значення для числових полів в specs
    for (const [key, val] of Object.entries(specs)) {
      const numeric = Number(val);
      if (!isNaN(numeric) && numeric < 0) {
        return `Specification "${key}" cannot be negative.`;
      }
    }

    return "";
  };

  const handleCreateComponent = async () => {
    const validationError = validateFields();
    if (validationError) {
      setError(validationError);
      return;
    }

    setError("");

    const formattedData = {
      ...componentData,
      price: Number(componentData.price),
      quantityOnStock: Number(componentData.quantityOnStock),
    };

    try {
      console.log("Creating:", formattedData);
      await axios.post("http://localhost:5153/api/Components", formattedData);
    } catch (err) {
      console.error(err);
    }
  };

  const renderSpecsInputs = () => {
    const field = (name, placeholder) => (
      <input
        key={name}
        name={name}
        placeholder={placeholder}
        onChange={handleSpecChange}
        required
      />
    );

    switch (componentData.category) {
      case ComponentType.CPU:
        return [
          field("cores", "Cores"),
          field("threads", "Threads"),
          field("frequency", "Base Frequency (GHz)"),
        ];
      case ComponentType.GPU:
        return [
          field("memory", "Memory (GB)"),
          field("clockSpeed", "Clock Speed (MHz)"),
          field("interface", "Interface"),
        ];
      case ComponentType.RAM:
        return [
          field("size", "Size (GB)"),
          field("type", "Type (DDR4/DDR5)"),
          field("speed", "Speed (MHz)"),
        ];
      case ComponentType.Motherboard:
        return [
          field("socket", "CPU Socket"),
          field("formFactor", "Form Factor"),
          field("chipset", "Chipset"),
        ];
      case ComponentType.HDD:
      case ComponentType.SSD:
        return [
          field("capacity", "Capacity (GB)"),
          field("interface", "Interface"),
          field("formFactor", "Form Factor"),
        ];
      case ComponentType.PSU:
        return [
          field("wattage", "Wattage (W)"),
          field("efficiency", "Efficiency Rating"),
          field("modularity", "Modularity (Yes/No)"),
        ];
      case ComponentType.Case:
        return [
          field("size", "Size (ATX/mATX)"),
          field("material", "Material"),
          field("color", "Color"),
        ];
      case ComponentType.CoolingSystem:
        return [
          field("type", "Type (Air/Liquid)"),
          field("fanSize", "Fan Size (mm)"),
          field("noiseLevel", "Noise Level (dB)"),
        ];
      default:
        return null;
    }
  };

  return (
    <section className="new-component-modal">
      <h2>Add New Component</h2>

      {error && <p className="error-message">{error}</p>}

      <input
        name="manufacturer"
        placeholder="Manufacturer"
        onChange={handleChange}
        required
      />
      <input
        name="model"
        placeholder="Model"
        onChange={handleChange}
        required
      />
      <input
        name="price"
        type="number"
        placeholder="Price"
        onChange={handleChange}
        required
      />
      <input
        name="quantityOnStock"
        type="number"
        placeholder="Quantity On Stock"
        onChange={handleChange}
        required
      />

      <select name="category" onChange={handleChange} required>
        <option value="">Select Category</option>
        {Object.entries(ComponentType).map(([key, val]) => (
          <option key={key} value={val}>
            {key}
          </option>
        ))}
      </select>

      <div className="specs-section">
        <h3>Specifications</h3>
        <div className="specs">{renderSpecsInputs()}</div>
      </div>

      <button className="button" onClick={handleCreateComponent}>
        Create Component
      </button>
    </section>
  );
}
