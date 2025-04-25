import axios from "axios";
import { ProductType } from "../enums/product-type.js";

const apiUrl = "http://localhost:5153/api/Product";

//createProduct
export const createProduct = async (product) => {
  try {
    checkIfUpsertProductValid(product);

    await axios.post(apiUrl, product);

    return true;
  } catch (err) {
    console.error(err);

    return false;
  }
};

//getProduct
export const getAllProducts = async () => {
  try {
    const response = await axios.get(apiUrl);
    return response.data;
  } catch (err) {
    console.error(err);
    return null;
  }
};

export const getProductById = async (productId) => {
  try {
    const response = await axios.get(`${apiUrl}/${productId}`);
    return response.data;
  } catch (err) {
    console.error(err);
    return null;
  }
};

export const geProductsByCategory = async (category) => {
  try {
    checkIfCategoryValid(category);

    const response = await axios.get(`${apiUrl}/by-category/${category}`);

    return response.data;
  } catch (err) {
    console.error(err);
    return null;
  }
};

//putProduct
export const updateProduct = async (productId, product) => {
  try {
    checkIfUpsertProductValid(product);

    await axios.put(`${apiUrl}/${productId}`, product);

    return true;
  } catch (err) {
    console.error(err);
    return false;
  }
};

//patchProduct
export const patchProduct = async (productId, product) => {
  try {
    checkIfPatchProductValid(product);

    await axios.patch(`${apiUrl}/${productId}`, product);

    return true;
  } catch (err) {
    console.error(err);
    return false;
  }
};

//deleteProduct
export const deleteProduct = async (productId) => {
  try {
    await axios.delete(`${apiUrl}/${productId}`);

    return true;
  } catch (err) {
    console.error(err);
    return false;
  }
};

//adictional functions
const checkIfUpsertProductValid = (product) => {
  if (
    product.category === undefined ||
    typeof product.category !== "string" ||
    !(product.category in ProductType)
  ) {
    throw new Error("Invalid product category");
  }
  if (
    product.componentId === undefined ||
    (product.componentId !== null && typeof product.componentId !== "number")
  ) {
    throw new Error("Invalid componentId");
  }
  if (
    product.computerId === undefined ||
    (product.computerId !== null && typeof product.computerId !== "number")
  ) {
    throw new Error("Invalid computerId");
  }
  if (product.imgUrl === undefined || typeof product.imgUrl !== "string") {
    throw new Error("Invalid image url");
  }
  if (
    product.category === ProductType.COMPONENT &&
    (product.componentId === undefined || product.componentId === null) &&
    product.computerId !== null
  ) {
    throw new Error("Invalid component product");
  }
  if (
    product.category === ProductType.COMPUTER &&
    (product.computerId === undefined || product.computerId === null) &&
    product.componentId !== null
  ) {
    throw new Error("Invalid computer product");
  }
};

const checkIfPatchProductValid = (product) => {
  if (typeof product.imgUrl !== "string") {
    throw new Error("Invalid image url");
  }
};

const checkIfCategoryValid = (category) => {
  if (category === undefined || typeof category !== "string") {
    throw new Error("Invalid category");
  }
  if (ProductType[category] === undefined) {
    throw new Error("Invalid product category");
  }
};
