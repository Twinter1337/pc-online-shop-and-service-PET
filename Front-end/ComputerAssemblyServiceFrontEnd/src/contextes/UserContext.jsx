import { createContext, useContext, useState, useEffect } from "react";
import axios from "axios";

const UserContext = createContext();

export const UserProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const [employee, setEmployee] = useState(null);

  const isAuthorized = !!user;

  useEffect(() => {
    const savedUser = localStorage.getItem("user");
    const savedEmployee = localStorage.getItem("employee");

    if (savedUser) {
      setUser(JSON.parse(savedUser));
    }

    if (savedEmployee) {
      setEmployee(JSON.parse(savedEmployee));
    }
  }, []);

  const login = async (userData) => {
    setUser(userData);
    localStorage.setItem("user", JSON.stringify(userData));

    try {
      const employeeRes = await axios.get(
        `http://localhost:5153/api/Employee/by-user-id/${userData.userId}`
      );

      if (employeeRes.data) {
        setEmployee(employeeRes.data);
        localStorage.setItem("employee", JSON.stringify(employeeRes.data));
      }
    } catch (err) {
      console.error("Failed to fetch employee info:", err);
    }
  };

  const logout = () => {
    setUser(null);
    setEmployee(null);
    localStorage.removeItem("user");
    localStorage.removeItem("employee");
  };

  return (
    <UserContext.Provider
      value={{ user, employee, isAuthorized, login, logout }}
    >
      {children}
    </UserContext.Provider>
  );
};

export const useUser = () => useContext(UserContext);
