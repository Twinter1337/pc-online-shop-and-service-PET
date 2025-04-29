import { useState, useEffect } from "react";
import "./StoreFilter.css";
import { ComponentType } from "../../../scripts/enums/component-type";
import axios from "axios";
import { ProductType } from "../../../scripts/enums/product-type";

export default function StoreFilter({ onFilterChange }) {
  const defaultFilters = {
    minPrice: "",
    maxPrice: "",
    productType: "",
    selectedComponents: [],
  };

  const [filters, setFilters] = useState(defaultFilters);
  const [componentsByCategory, setComponentsByCategory] = useState({});

  useEffect(() => {
    const fetchComponents = async () => {
      const res = await axios.get("http://localhost:5153/api/Components");
      const grouped = {};

      Object.values(ComponentType).forEach((type) => {
        grouped[type] = res.data.filter((c) => c.category === type);
      });

      setComponentsByCategory(grouped);
    };

    fetchComponents();
  }, []);

  const handleBasicChange = (e) => {
    const { name, value } = e.target;
    const updated = { ...filters, [name]: value };
    setFilters(updated);
    onFilterChange(updated);
  };

  const handleCheckboxChange = (componentId) => {
    const updatedComponents = filters.selectedComponents.includes(componentId)
      ? filters.selectedComponents.filter((id) => id !== componentId)
      : [...filters.selectedComponents, componentId];

    const updated = { ...filters, selectedComponents: updatedComponents };
    setFilters(updated);
    onFilterChange(updated);
  };

  const handleReset = () => {
    setFilters(defaultFilters);
    onFilterChange(defaultFilters);
  };

  return (
    <aside className="store-filter">
      <h3>Filter Products</h3>
      <div className="line-br"></div>

      <section className="filter-section">
        <label>Price</label>
        <div className="price-filter">
          <input
            type="number"
            name="minPrice"
            placeholder="Min"
            value={filters.minPrice}
            onChange={handleBasicChange}
          />
          <span>-</span>
          <input
            type="number"
            name="maxPrice"
            placeholder="Max"
            value={filters.maxPrice}
            onChange={handleBasicChange}
          />
        </div>
      </section>

      <section className="filter-section">
        <label>Product Type</label>
        <select
          name="productType"
          value={filters.productType}
          onChange={handleBasicChange}
        >
          <option value="">All</option>
          {Object.entries(ProductType).map(([key, val]) => (
            <option key={key} value={val}>
              {key}
            </option>
          ))}
        </select>
      </section>

      <section className="filter-section">
        <h4>Filter by Components</h4>
        <div className="line-br"></div>
        {Object.entries(componentsByCategory).map(([category, components]) => (
          <div key={category} className="component-group">
            <details>
              <summary>{category}</summary>
              <ul>
                {components.map((component) => (
                  <li key={component.componentId}>
                    <label>
                      <span>{component.model}</span>
                      <input
                        type="checkbox"
                        checked={filters.selectedComponents.includes(
                          component.componentId
                        )}
                        onChange={() =>
                          handleCheckboxChange(component.componentId)
                        }
                      />
                    </label>
                  </li>
                ))}
              </ul>
            </details>
          </div>
        ))}
      </section>
      <div className="line-br"></div>

      <button className="button reset-button" onClick={handleReset}>
        Reset Filters
      </button>
    </aside>
  );
}
