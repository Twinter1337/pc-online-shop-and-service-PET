import axios from "axios";
import { ComponentType } from "../enums/component-type.js";

const apiUrl = "http://localhost:5153/api/Components";

export const createComponent = async (
  manufacturer,
  model,
  price,
  category,
  quantityOnStock,
  specs
) => {
  const component = {
    manufacturer: manufacturer,
    model: model,
    price: price,
    category: category,
    quantityOnStock,
    specs: specs,
  };

  try {
    checkIfComponentValid(component);

    await axios.post(apiUrl, component);

    return true;
  } catch (err) {
    console.error(err);

    return false;
  }
};

export const getAllComponents = async () => {
  try {
    const response = await axios.get(apiUrl);
    return response.data;
  } catch (err) {
    console.error(err);
    return null;
  }
};

export const getComponentById = async (componentId) => {
  try {
    const response = await axios.get(`${apiUrl}/${componentId}`);
    return response.data;
  } catch (err) {
    console.error(err);
    return null;
  }
};

export const getFilteredComponents = async (
  category,
  minPrice,
  maxPrice,
  manufacturer,
  model
) => {
  const filter = {
    Category: category,
    MinPrice: minPrice,
    MaxPrice: maxPrice,
    Manufacturer: manufacturer,
    Model: model,
  };

  try {
    checkIfFilterValid(filter);

    const cleanFilter = Object.fromEntries(
      Object.entries(filter).filter(([_, v]) => v !== null && v !== undefined)
    );

    const query = new URLSearchParams(cleanFilter).toString();

    const response = await axios.get(`${apiUrl}/filter?${query}`);
    return response.data;
  } catch (err) {
    console.error(err);
    return null;
  }
};

// export const updateComponent = async (
//   componentId,
//   manufacturer,
//   model,
//   price,
//   category,
//   quantityOnStock,
//   specs
// ) => {
//   const component = {
//     componentId: componentId,
//     manufacturer: manufacturer,
//     model: model,
//     price: price,
//     category: category,
//     quantityOnStock: quantityOnStock,
//     specs: Object.keys(specs).length === 0 ? null : specs,
//   };

//   try {
//     checkIfComponentValid(component);

//     await axios.put(`${apiUrl}/${componentId}`, component);
//     return true;
//   } catch (err) {
//     console.error(err);
//     return false;
//   }
// };

const checkIfComponentValid = (component) => {
  if (typeof component.manufacturer !== "string") {
    throw new Error("Manufacturer must be a string");
  }
  if (typeof component.model !== "string") {
    throw new Error("Model must be a string");
  }
  if (typeof component.price !== "number") {
    throw new Error("Price must be a number");
  }
  if (
    typeof component.category !== "string" ||
    ComponentType[component.category] === undefined
  ) {
    throw new Error("Category must be a string and a valid component type");
  }
  if (typeof component.quantityOnStock !== "number") {
    throw new Error("Quantity on stock must be a number");
  }
  if (typeof component.specs !== "object") {
    throw new Error("Specs must be an object");
  }
};

const checkIfFilterValid = (filter) => {
  if (
    filter.Category !== null &&
    (typeof filter.Category !== "string" || !(filter.Category in ComponentType))
  ) {
    throw new Error("Category must be a valid component type string or null");
  }
  if (typeof filter.MinPrice !== "number" && filter.MinPrice !== null) {
    throw new Error("Min price must be a number or null");
  }
  if (typeof filter.MaxPrice !== "number" && filter.MaxPrice !== null) {
    throw new Error("Max price must be a number or null");
  }
  if (typeof filter.Manufacturer !== "string" && filter.Manufacturer !== null) {
    throw new Error("Manufacturer must be a string or null");
  }
  if (typeof filter.Model !== "string" && filter.Model !== null) {
    throw new Error("Model must be a string or null");
  }
  if (filter.MinPrice > filter.MaxPrice) {
    throw new Error("Min price must be less than max price");
  }
  if (filter.MinPrice < 0) {
    throw new Error("Min price must be greater than 0");
  }
  if (filter.MaxPrice < 0) {
    throw new Error("Max price must be greater than 0");
  }
};
