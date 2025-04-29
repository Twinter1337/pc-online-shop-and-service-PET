import axios from "axios";
import { OrderStatus } from "../enums/order-status";

const apiUrl = "http://localhost:5153/api/";

const createOrder = async (user, isAuthorized) => {
  try {
    const newOrder = { clientId: isAuthorized ? user.userId : null };
    const response = await axios.post(`${apiUrl}Order`, newOrder);
    return response.data;
  } catch (err) {
    console.error("Error creating order:", err);
    throw err;
  }
};

const updateOrderItem = async (orderItemId, updatedOrderItem) => {
  try {
    const response = await axios.patch(
      `${apiUrl}OrderItem/${orderItemId}`,
      updatedOrderItem
    );
    return response.data;
  } catch (err) {
    console.error("Error updating order item:", err);
    throw err;
  }
};

const createOrderItem = async (orderItem) => {
  try {
    const response = await axios.post(`${apiUrl}OrderItem`, orderItem);
    return response.data;
  } catch (err) {
    console.error("Error creating order item:", err);
    throw err;
  }
};

const processOrderItem = async (user, isAuthorized, productId, price) => {
  try {
    const order = await getOrder(user, isAuthorized);
    if (order) {
      const existingOrderItem = await findExistingOrderItem(
        order.orderId,
        productId
      );
      if (existingOrderItem) {
        return await updateOrderItemQuantity(
          existingOrderItem.itemId,
          existingOrderItem.quantity + 1
        );
      } else {
        return await createOrderItem({
          orderId: order.orderId,
          productId,
          quantity: 1,
          price,
        });
      }
    }
  } catch (err) {
    console.error("Error processing order item:", err);
    if (err.message === "No existing order found") {
      const newOrder = await createOrder(user, isAuthorized);
      return await createOrderItem({
        orderId: newOrder.orderId,
        productId,
        quantity: 1,
        price,
      });
    }
    throw err;
  }
};

const getOrder = async (user, isAuthorized) => {
  try {
    const params = new URLSearchParams({
      clientId: isAuthorized ? user.userId : null,
      status: OrderStatus.New,
    });

    const url = `${apiUrl}Order/filter?${params.toString()}`;
    console.log("Generated URL:", url);

    const response = await axios.get(url);

    if (response.data.length > 0) {
      return response.data[0];
    } else {
      throw new Error("No existing order found");
    }
  } catch (err) {
    if (err.response && err.response.status === 404) {
      throw new Error("No existing order found");
    }
    console.error("Error fetching order:", err);
    throw err;
  }
};

const findExistingOrderItem = async (orderId, productId) => {
  try {
    const response = await axios.get(`${apiUrl}OrderItem/filter`, {
      params: { orderId, productId },
    });
    return response.data.length > 0 ? response.data[0] : null;
  } catch (err) {
    console.error("Error fetching order item:", err);
    throw err;
  }
};

export const updateOrderItemQuantity = async (itemId, newQuantity) => {
  const updatedOrderItem = { quantity: newQuantity };
  return await updateOrderItem(itemId, updatedOrderItem);
};

export const addOrderItem = async (user, isAuthorized, productId, price) => {
  return await processOrderItem(user, isAuthorized, productId, price);
};

export const getOrdersByUserId = async (userId) => {
  try {
    const response = await axios.get(
      `${apiUrl}Order/filter?clientId=${userId}`
    );
    const orders = response.data;

    const ordersWithDetails = await Promise.all(
      orders.map(async (order) => {
        const [itemsRes, servicesRes] = await Promise.all([
          axios.get(`${apiUrl}OrderItem/by-order/${order.orderId}`),
          axios.get(`${apiUrl}OrderService/by-order-id/${order.orderId}`),
        ]);

        const orderItems = await Promise.all(
          itemsRes.data.map(async (item) => {
            const productRes = await axios.get(
              `${apiUrl}Product/${item.productId}`
            );
            return {
              ...item,
              product: productRes.data,
            };
          })
        );

        const orderServices = await Promise.all(
          servicesRes.data.map(async (service) => {
            const serviceRes = await axios.get(
              `${apiUrl}Service/${service.serviceId}`
            );
            return {
              ...service,
              service: serviceRes.data,
            };
          })
        );

        return {
          ...order,
          orderItems,
          orderServices,
        };
      })
    );

    return ordersWithDetails;
  } catch (err) {
    console.error("Error fetching full orders:", err);
    throw err;
  }
};

export const deleteOrderItem = async (itemId) => {
  try {
    await axios.delete(`${apiUrl}OrderItem/${itemId}`);
    return true;
  } catch (err) {
    console.error(err);
    return false;
  }
};

export const createOrderService = async (orderService) => {
  try {
    const responce = await axios.post(`${apiUrl}OrderService`, orderService);
    return responce.data;
  } catch (err) {
    console.error(err);
  }
};

const processOrderService = async (user, isAuthorized, serviceId) => {
  try {
    const order = await getOrder(user, isAuthorized);

    if (order) {
      return await createOrderService({
        orderId: order.orderId,
        serviceId: serviceId,
        responsibleEmployeeId: null,
      });
    }
  } catch (err) {
    if (err.message === "No existing order found") {
      const newOrder = await createOrder(user, isAuthorized);
      return await createOrderService({
        orderId: newOrder.orderId,
        serviceId: serviceId,
        responsibleEmployeeId: null,
      });
    }
  }
};

export const addOrderService = async (user, isAuthorized, serviceId) => {
  return await processOrderService(user, isAuthorized, serviceId);
};

export const deleteOrderService = async (orderServiceId) => {
  try {
    await axios.delete(`${apiUrl}OrderService/${orderServiceId}`);
  } catch (err) {
    console.error(err);
  }
};
