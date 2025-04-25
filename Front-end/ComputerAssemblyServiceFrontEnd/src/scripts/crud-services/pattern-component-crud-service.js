import axios from "axios";
import { ComponentType } from "../enums/component-type.js";

const apiUrl = "http://localhost:5153/api/PatternComponent";

//create
export const createPatternComponent = async (patternComponent) => {
  try {
    checkIfUpsertPatternComponentValid(patternComponent);

    await axios.post(apiUrl, patternComponent);

    return true;
  } catch (err) {
    console.error(err);

    return false;
  }
};

//get
export const getAllPatternComponents = async () => {
  try {
    const response = await axios.get(apiUrl);
    return response.data;
  } catch (err) {
    console.error(err);
    return null;
  }
};

export const getPatternComponentById = async (patternComponentId) => {
  try {
    const response = await axios.get(`${apiUrl}/${patternComponentId}`);
    return response.data;
  } catch (err) {
    console.error(err);
    return null;
  }
};

export const getPatternComponentByPrebuildPatternId = async (
  prebuildPatternId
) => {
  try {
    const response = await axios.get(
      `${apiUrl}/by-prebuild/${prebuildPatternId}`
    );
    return response.data;
  } catch (err) {
    console.error(err);
    return null;
  }
};

//put
export const updatePatternComponents = async (
  patternComponentId,
  patternComponent
) => {
  try {
    checkIfUpsertPatternComponentValid(patternComponent);

    await axios.put(`${apiUrl}/${patternComponentId}`, patternComponent);

    return true;
  } catch (err) {
    console.error(err);
    return false;
  }
};

//patch
export const patchPatternComponent = async (
  patternComponentId,
  patternComponent
) => {
  try {
    checkIfPatchPatternComponentValid(patternComponent);

    await axios.patch(`${apiUrl}/${patternComponentId}`, patternComponent);

    return true;
  } catch (err) {
    console.error(err);
    return false;
  }
};

//delete
export const deletePatternComponent = async (patternComponentId) => {
  try {
    await axios.delete(`${apiUrl}/${patternComponentId}`);
    return true;
  } catch (err) {
    console.error(err);
    return false;
  }
};

//adictional functions
const checkIfUpsertPatternComponentValid = (patternComponent) => {
  if (
    patternComponent.patternId === undefined ||
    typeof patternComponent.patternId !== "number" ||
    patternComponent.patternId === null
  ) {
    throw new Error("Invalid pattern id ");
  }
  if (
    patternComponent.componentId === undefined ||
    typeof patternComponent.componentId !== "number" ||
    patternComponent.componentId === null
  ) {
    throw new Error("Invalid component id ");
  }
  if (
    patternComponent.quantity === undefined ||
    typeof patternComponent.quantity !== "number" ||
    patternComponent.quantity === null
  ) {
    throw new Error("Invalid quantity");
  }
  if (patternComponent.quantity <= 0) {
    throw new Error("Invalid quantity");
  }
};

const checkIfPatchPatternComponentValid = (patternComponent) => {
  if (
    (patternComponent.patternId !== undefined &&
      typeof patternComponent.patternId !== "number") ||
    patternComponent.patternId === null
  ) {
    throw new Error("Invalid pattern id ");
  }
  if (
    (patternComponent.componentId !== undefined &&
      typeof patternComponent.componentId !== "number") ||
    patternComponent.componentId === null
  ) {
    throw new Error("Invalid component id ");
  }
  if (
    (patternComponent.quantity !== undefined &&
      typeof patternComponent.quantity !== "number") ||
    patternComponent.quantity === null
  ) {
    throw new Error("Invalid quantity");
  }
  if (patternComponent.quantity <= 0) {
    throw new Error("Invalid quantity");
  }
};
