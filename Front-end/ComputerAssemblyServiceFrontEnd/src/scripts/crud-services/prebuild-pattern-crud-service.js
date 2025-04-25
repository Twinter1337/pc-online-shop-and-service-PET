import axios from "axios";

const apiUrl = "http://localhost:5153/api/PrebuildPattern";

//create
export const createPrebuildPattern = async (prebuildPattern) => {
  try {
    checkIfUpsertPrebuildPatternValid(prebuildPattern);

    await axios.post(apiUrl, prebuildPattern);

    return true;
  } catch (err) {
    console.error(err);

    return false;
  }
};

//get
export const getAllPrebuildPatterns = async () => {
  try {
    const response = await axios.get(apiUrl);
    return response.data;
  } catch (err) {
    console.error(err);
    return null;
  }
};

export const getPrebuildPatternById = async (prebuildPatternId) => {
  try {
    const response = await axios.get(`${apiUrl}/${prebuildPatternId}`);
    return response.data;
  } catch (err) {
    console.error(err);
    return null;
  }
};

export const getFilteredPrebuildPatterns = async (filter) => {
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

//put
export const updatePrebuildPattern = async (
  prebuildPatternId,
  prebuildPattern
) => {
  try {
    checkIfUpsertPrebuildPatternValid(prebuildPattern);

    await axios.put(`${apiUrl}/${prebuildPatternId}`, prebuildPattern);

    return true;
  } catch (err) {
    console.error(err);
    return false;
  }
};

//patch
export const patchPrebuildPattern = async (
  prebuildPatternId,
  prebuildPattern
) => {
  try {
    checkIfPatchPrebuildPatternValid(prebuildPattern);

    await axios.patch(`${apiUrl}/${prebuildPatternId}`, prebuildPattern);

    return true;
  } catch (err) {
    console.error(err);
    return false;
  }
};

//delete
export const deletePrebuildPattern = async (prebuildPatternId) => {
  try {
    await axios.delete(`${apiUrl}/${prebuildPatternId}`);
    return true;
  } catch (err) {
    console.error(err);
    return false;
  }
};

//adictional functions
const checkIfUpsertPrebuildPatternValid = (prebuildPattern) => {
  if (
    prebuildPattern.prebuildName === undefined ||
    typeof prebuildPattern.prebuildName !== "string"
  ) {
    throw new Error("Prebuild pattern name must be a string");
  }
  if (
    prebuildPattern.manufacturer === undefined ||
    typeof prebuildPattern.manufacturer !== "string"
  ) {
    throw new Error("Prebuild pattern manufacturer must be a string");
  }
  if (
    prebuildPattern.description === undefined ||
    typeof prebuildPattern.description !== "string"
  ) {
    throw new Error("Prebuild pattern description must be a string");
  }
  if (
    prebuildPattern.basePrice === undefined ||
    typeof prebuildPattern.basePrice !== "number"
  ) {
    throw new Error("Prebuild pattern price must be a number");
  }
  if (prebuildPattern.basePrice < 0) {
    throw new Error("Prebuild pattern price must be greater than 0");
  }
};

const checkIfPatchPrebuildPatternValid = (prebuildPattern) => {
  if (
    prebuildPattern.prebuildName !== undefined &&
    typeof prebuildPattern.prebuildName !== "string"
  ) {
    throw new Error("Prebuild pattern name must be a string");
  }
  if (
    prebuildPattern.manufacturer !== undefined &&
    typeof prebuildPattern.manufacturer !== "string"
  ) {
    throw new Error("Prebuild pattern manufacturer must be a string");
  }
  if (
    prebuildPattern.description !== undefined &&
    typeof prebuildPattern.description !== "string"
  ) {
    throw new Error("Prebuild pattern description must be a string");
  }
  if (
    prebuildPattern.basePrice !== undefined &&
    typeof prebuildPattern.basePrice !== "number"
  ) {
    throw new Error("Prebuild pattern price must be a number");
  }
  if (
    prebuildPattern.basePrice !== undefined &&
    prebuildPattern.basePrice < 0
  ) {
    throw new Error("Prebuild pattern price must be greater than 0");
  }
};

const checkIfFilterValid = (filter) => {
  if (
    (filter.minPrice !== undefined && typeof filter.minPrice !== "number") ||
    filter.minPrice === null
  ) {
    throw new Erorr("Invalid min price of filter");
  }
  if (
    (filter.maxPrice !== undefined && typeof filter.maxPrice !== "number") ||
    filter.maxPrice === null
  ) {
    throw new Erorr("Invalid max price of filter");
  }
  if (
    (filter.manufacturer !== undefined &&
      typeof filter.manufacturer !== "string") ||
    filter.manufacturer === null
  ) {
    throw new Erorr("Invalid manufacturer of filter");
  }
  if (filter.minPrice > filter.maxPrice) {
    throw new Error("Min price must be lower than max price");
  }
};
