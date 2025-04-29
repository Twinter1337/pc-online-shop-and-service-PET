import { useEffect, useState } from "react";
import "./NewPrebuildPatternModal.css";
import * as componentCrudService from "../../../scripts/crud-services/component-crud-service";
import { ComponentType } from "../../../scripts/enums/component-type";
import { ProductType } from "../../../scripts/enums/product-type";
import * as prebuildPatternCrudService from "../../../scripts/crud-services/prebuild-pattern-crud-service";
import * as patternComponentCrudService from "../../../scripts/crud-services/pattern-component-crud-service";
import * as productCrudService from "../../../scripts/crud-services/product-crud-service";

const componentFields = [
  { label: "CPU", name: "cpu", type: ComponentType.CPU },
  { label: "GPU", name: "gpu", type: ComponentType.GPU },
  { label: "RAM", name: "ram", type: ComponentType.RAM },
  { label: "PSU", name: "psu", type: ComponentType.PSU },
  { label: "Case", name: "case", type: ComponentType.Case },
  {
    label: "Motherboard",
    name: "motherboard",
    type: ComponentType.Motherboard,
  },
  {
    label: "Cooling System",
    name: "coolingSystem",
    type: ComponentType.CoolingSystem,
  },
  { label: "HDD", name: "hdd", type: ComponentType.HDD },
  { label: "SSD", name: "ssd", type: ComponentType.SSD },
];

export default function NewPrebuildPatternModal({ closeModal }) {
  const [componentsByType, setComponentsByType] = useState({});
  const [selectedComponents, setSelectedComponents] = useState({});
  const [formFields, setFormFields] = useState({
    prebuildName: "",
    imgUrl: "",
    manufacturer: "",
    description: "",
    productType: "",
  });

  useEffect(() => {
    const fetchComponents = async () => {
      try {
        const allComponents = await componentCrudService.getAllComponents();
        const grouped = {};
        componentFields.forEach(({ name, type }) => {
          grouped[name] = allComponents.filter((c) => c.category === type);
        });
        setComponentsByType(grouped);
      } catch (err) {
        console.error(err);
      }
    };

    fetchComponents();
  }, []);

  const handleComponentChange = (e) => {
    const { name, value } = e.target;
    setSelectedComponents((prev) => ({ ...prev, [name]: Number(value) }));
  };

  const handleFieldChange = (e) => {
    const { name, value } = e.target;
    setFormFields((prev) => ({ ...prev, [name]: value }));
  };

  const renderInput = (label, name, type = "text") => (
    <div className="form-group">
      <label htmlFor={name}>{label}</label>
      <input
        type={type}
        id={name}
        name={name}
        placeholder={`Enter ${label.toLowerCase()}`}
        value={formFields[name]}
        onChange={handleFieldChange}
        required
      />
    </div>
  );

  const renderSelect = (label, name, options) => (
    <div className="form-group">
      <label htmlFor={name}>{label}</label>
      <select
        name={name}
        value={selectedComponents[name] || ""}
        onChange={handleComponentChange}
        required
      >
        <option value="">Select {label}</option>
        {options.map((option) => (
          <option key={option.componentId} value={option.componentId}>
            {option.model}
          </option>
        ))}
      </select>
    </div>
  );

  const renderProductTypeSelect = () => (
    <div className="form-group">
      <label htmlFor="productType">Product Type</label>
      <select
        id="productType"
        name="productType"
        value={formFields.productType}
        onChange={handleFieldChange}
        required
      >
        <option value="">Select Product Type</option>
        {Object.entries(ProductType).map(([key, value]) => (
          <option key={key} value={value}>
            {key}
          </option>
        ))}
      </select>
    </div>
  );

  const validateFields = () => {
    const areTextFieldsFilled = Object.values(formFields).every(
      (value) => value.trim() !== ""
    );
    const areComponentsSelected = componentFields.every(
      ({ name }) => selectedComponents[name] && selectedComponents[name] !== -1
    );

    return areTextFieldsFilled && areComponentsSelected;
  };

  const handleCreatePrebuildPattern = async () => {
    if (!validateFields()) {
      alert("Please fill out all fields and select all components!");
      return;
    }

    try {
      const newPrebuild =
        await prebuildPatternCrudService.createPrebuildPattern({
          prebuildName: formFields.prebuildName,
          manufacturer: formFields.manufacturer,
          description: formFields.description,
          basePrice: 0,
        });

      console.log(newPrebuild);

      for (const key in selectedComponents) {
        const componentId = selectedComponents[key];
        if (componentId !== -1) {
          await patternComponentCrudService.createPatternComponent({
            patternId: newPrebuild.serialNumber,
            componentId: componentId,
            quantity: 1,
          });
        }
      }

      await productCrudService.createProduct({
        computerId: newPrebuild.serialNumber,
        category: formFields.productType,
        imgUrl: formFields.imgUrl,
      });

      closeModal();
      // alert("Prebuild created successfully!");
    } catch (err) {
      console.error(err);
      // alert("Failed to create prebuild!");
    }
  };

  return (
    <section className="new-prebuild-modal">
      <h2>Create New Prebuild</h2>
      <div className="line-br" />

      {renderInput("Prebuild Name", "prebuildName")}
      {renderInput("Image URL", "imgUrl")}
      {renderInput("Manufacturer", "manufacturer")}
      {renderInput("Description", "description")}
      {renderProductTypeSelect()}

      <div className="components-selection">
        {componentFields.map(({ label, name }) =>
          renderSelect(label, name, componentsByType[name] || [])
        )}
      </div>

      <div className="line-br" />

      <button
        className="button create-prebuild-button"
        onClick={handleCreatePrebuildPattern}
      >
        Create Prebuild
      </button>
    </section>
  );
}
