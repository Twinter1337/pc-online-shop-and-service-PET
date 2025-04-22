import axios from "axios";

const apiUrl = "http://localhost:5153/api/Product";

//GETS
export const getAllProducts = async () => {
  const response = await axios.get(apiUrl);
  return response.data;
};

export const getProductById = async (productId) => {
  const response = await axios.get(`${apiUrl}/${productId}`);
  return response.data;
};

export const getProductsByCategory = async (category) => {
  const response = await axios.get(`${apiUrl}/by-category/${category}`);
  return response.data;
};

//CREATE
export const createProduct = async (
  componentId,
  computerId,
  category,
  imgUrl
) => {
  const product = {
    componentId: componentId,
    computerId: computerId,
    category: category,
    imgUrl: imgUrl,
  };

  try {
    const response = await axios.post(apiUrl, product);
    console.log("Product created");
    return response.data;
  } catch (err) {
    console.error(err);
  }
};

//UPDATE
export const updateProduct = async () => {
  const response = await axios.put(apiUrl);
  return response.data;
};
