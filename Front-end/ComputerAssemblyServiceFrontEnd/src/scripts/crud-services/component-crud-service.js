import axios from "axios";
import { ComponentType } from "../enums/component-type.js";

const apiUrl = "http://localhost:5153/api/Components";

//createComponent
export const createComponent = async (component) => {
  try {
    checkIfUpsertComponentValid(component);

    await axios.post(apiUrl, component);

    return true;
  } catch (err) {
    console.error(err);

    return false;
  }
};

//getComponent
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

export const getFilteredComponents = async (filter) => {
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

//putComponent
export const updateComponent = async (componentId, component) => {
  try {
    checkIfUpsertComponentValid(component);

    await axios.put(`${apiUrl}/${componentId}`, component);

    return true;
  } catch (err) {
    console.error(err);
    return false;
  }
};

//patchComponent
export const patchComponent = async (componentId, component) => {
  try {
    checkIfPatchComponentValid(component);

    await axios.patch(`${apiUrl}/${componentId}`, component);

    return true;
  } catch (err) {
    console.error(err);
    return false;
  }
};

//deleteComponent
export const deleteComponent = async (componentId) => {
  try {
    await axios.delete(`${apiUrl}/${componentId}`);
    return true;
  } catch (err) {
    console.error(err);
    return false;
  }
};

//adictional functions
const checkIfUpsertComponentValid = (component) => {
  if (
    typeof component.manufacturer !== "string" ||
    component.manufacturer === undefined
  ) {
    throw new Error("Manufacturer must be a string");
  }
  if (typeof component.model !== "string" || component.model === undefined) {
    throw new Error("Model must be a string");
  }
  if (typeof component.price !== "number" || component.price === undefined) {
    throw new Error("Price must be a number");
  }
  if (
    component.category === undefined ||
    typeof component.category !== "string" ||
    ComponentType[component.category] === undefined
  ) {
    console.log(component.category);
    throw new Error("Category must be a string and a valid component type");
  }
  if (
    typeof component.quantityOnStock !== "number" ||
    component.quantityOnStock === undefined
  ) {
    throw new Error("Quantity on stock must be a number");
  }
  if (
    typeof component.characteristics !== "object" ||
    component.characteristics === undefined
  ) {
    throw new Error("Specs must be an object");
  }
};

const checkIfPatchComponentValid = (component) => {
  if (
    component.manufacturer !== undefined &&
    typeof component.manufacturer !== "string"
  ) {
    throw new Error("Manufacturer must be a string");
  }
  if (component.model !== undefined && typeof component.model !== "string") {
    throw new Error("Model must be a string");
  }
  if (component.price !== undefined && typeof component.price !== "number") {
    throw new Error("Price must be a number");
  }
  if (
    component.category !== undefined &&
    (typeof component.category !== "string" ||
      ComponentType[component.category] === undefined)
  ) {
    throw new Error("Category must be a string and a valid component type");
  }
  if (
    component.quantityOnStock !== undefined &&
    typeof component.quantityOnStock !== "number"
  ) {
    throw new Error("Quantity on stock must be a number");
  }
  if (
    component.characteristics !== undefined &&
    typeof component.characteristics !== "object"
  ) {
    throw new Error("Specs must be an object");
  }
};

const checkIfFilterValid = (filter) => {
  if (
    filter.Category !== null &&
    (typeof filter.Category !== "string" ||
      !(filter.Category in ComponentType)) &&
    filter.Category !== undefined
  ) {
    throw new Error("Category must be a valid component type string or null");
  }
  if (
    typeof filter.MinPrice !== "number" &&
    filter.MinPrice !== null &&
    filter.MinPrice !== undefined
  ) {
    throw new Error("Min price must be a number or null");
  }
  if (
    typeof filter.MaxPrice !== "number" &&
    filter.MaxPrice !== null &&
    filter.MaxPrice !== undefined
  ) {
    throw new Error("Max price must be a number or null");
  }
  if (
    typeof filter.Manufacturer !== "string" &&
    filter.Manufacturer !== null &&
    filter.Manufacturer !== undefined
  ) {
    throw new Error("Manufacturer must be a string or null");
  }
  if (
    typeof filter.Model !== "string" &&
    filter.Model !== null &&
    filter.Model !== undefined
  ) {
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
