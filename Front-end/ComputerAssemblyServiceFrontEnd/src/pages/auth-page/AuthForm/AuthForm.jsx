import { useState } from "react";
import { motion, AnimatePresence } from "framer-motion";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { useUser } from "../../../contextes/UserContext";

import "./AuthForm.css";
import { UserRole } from "../../../scripts/enums/user-role";
import { addOrderItem } from "../../../scripts/services/order-service.js";

const AuthForm = () => {
  const [step, setStep] = useState(1);
  const [email, setEmail] = useState("");
  const { login } = useUser();
  const navigate = useNavigate();

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm();

  const renderInput = (label, name, type = "text", rules = {}) => (
    <div className="form-group">
      <label className="form-label">{label}</label>
      <input type={type} {...register(name, rules)} className="form-input" />
      {errors[name] && <p className="error-message">{errors[name].message}</p>}
    </div>
  );

  const backStep = () => {
    setStep((prevStep) => {
      if (prevStep === 1) return prevStep;
      if (prevStep === 2) return 1;
      if (prevStep === 3) return 1;
      return prevStep;
    });
  };

  const onSubmitStep1 = async (data) => {
    const { email } = data;
    setEmail(email);

    try {
      const userInDb = await axios.get(
        `http://localhost:5153/api/User/by-email/${email}`
      );
      console.log("User exists:", userInDb.data);
      setStep(3);
      await axios.post("http://localhost:5153/auth/request-otp", { email });
    } catch (error) {
      if (error.response?.status === 404) {
        console.log("User not found, go to step 2");
        setStep(2);
      } else {
        console.error("Error checking user:", error.message);
      }
    }
  };

  const onSubmitStep2 = async (data) => {
    const newUser = {
      firstName: data.firstName,
      lastName: data.lastName,
      email,
      phoneNumber: null,
      role: UserRole.Client,
    };

    try {
      const res = await axios.post("http://localhost:5153/api/User/", newUser);
      if (res.status === 201) {
        setStep(3);
        await axios.post("http://localhost:5153/auth/request-otp", { email });
      }
    } catch (error) {
      console.error("Error creating user:", error.message);
    }
  };

  const onSubmitStep3 = async (data) => {
    try {
      const res = await axios.post("http://localhost:5153/auth/verify-otp", {
        email,
        code: data.code,
      });

      if (res.status === 200) {
        console.log("OTP verified successfully!");

        const userResponse = await axios.get(
          `http://localhost:5153/api/User/by-email/${email}`
        );

        login(userResponse.data);
        console.log("User data:", userResponse.data);

        const pendingItem = localStorage.getItem("pendingCartItem");
        if (pendingItem) {
          const { productId, price } = JSON.parse(pendingItem);

          try {
            await addOrderItem(userResponse.data, true, productId, price);
            console.log("Pending item successfully added to cart");
          } catch (error) {
            console.error("Failed to add pending item:", error.message);
          }

          localStorage.removeItem("pendingCartItem");
        }

        navigate("/user-page");
      }
    } catch (error) {
      console.error("Error verifying OTP:", error.message);
    }
  };

  const renderStep = () => {
    switch (step) {
      case 1:
        return (
          <motion.div key="step1" {...stepAnimationProps} className="auth-step">
            <h2 className="auth-title">Enter your email</h2>
            <form onSubmit={handleSubmit(onSubmitStep1)} className="auth-form">
              {renderInput("Email", "email", "email", {
                required: "The field is required",
                pattern: {
                  value: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
                  message: "Invalid email",
                },
              })}
              <button type="submit" className="button">
                Continue
              </button>
            </form>
          </motion.div>
        );
      case 2:
        return (
          <motion.div key="step2" {...stepAnimationProps} className="auth-step">
            <h2 className="auth-title">Sign In</h2>
            <form onSubmit={handleSubmit(onSubmitStep2)} className="auth-form">
              {renderInput("First name*", "firstName", "text", {
                required: "The field is required",
              })}
              {renderInput("Last name*", "lastName", "text", {
                required: "The field is required",
              })}
              <button type="submit" className="button">
                Continue
              </button>
              <button type="button" className="button" onClick={backStep}>
                Back
              </button>
            </form>
          </motion.div>
        );
      case 3:
        return (
          <motion.div key="step3" {...stepAnimationProps} className="auth-step">
            <h2 className="auth-title">Enter the code</h2>
            <p className="email-notice">Code sent to {email}</p>
            <form onSubmit={handleSubmit(onSubmitStep3)} className="auth-form">
              {renderInput("Verification code", "code", "text", {
                required: "The field is required",
                minLength: { value: 5, message: "Code must be 5 characters" },
              })}
              <button type="submit" className="button">
                Submit
              </button>
              <button type="button" className="button" onClick={backStep}>
                Back
              </button>
            </form>
          </motion.div>
        );
      default:
        return null;
    }
  };

  const stepAnimationProps = {
    initial: { opacity: 0, x: 50 },
    animate: { opacity: 1, x: 0 },
    exit: { opacity: 0, x: -50 },
    transition: { duration: 0.3 },
  };

  return (
    <motion.div
      key="form-wrapper"
      initial={{ opacity: 0, scale: 0.95 }}
      animate={{ opacity: 1, scale: 1 }}
      exit={{ opacity: 0, scale: 0.95 }}
      transition={{ duration: 0.3 }}
      className="auth-form-wrapper"
    >
      <AnimatePresence mode="wait">{renderStep()}</AnimatePresence>
    </motion.div>
  );
};

export default AuthForm;
