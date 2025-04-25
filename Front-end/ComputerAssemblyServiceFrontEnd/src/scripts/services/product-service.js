import { ComponentType } from "../enums/component-type.js";

export const getProductComponentModel = (category, components) => {
  try {
    checkIfCategoryValid(category);
    const component = components.find((c) => c.category === category);
    return component ? component.model : "None";
  } catch (err) {
    console.error(err);
    return "None";
  }
};

const checkIfCategoryValid = (category) => {
  if (category === null || category === undefined) {
    throw new Error("Category is required");
  }
  if (typeof category !== "string" || !(category in ComponentType)) {
    throw new Error("Invalid category");
  }
};
