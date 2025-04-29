import { useState, useEffect } from "react";
import "./PaymentForm.css";

export default function PaymentForm({ onSubmit, user }) {
  const [cardNumber, setCardNumber] = useState("");
  const [expiryDate, setExpiryDate] = useState("");
  const [cvv, setCvv] = useState("");
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");

  useEffect(() => {
    if (user && user.firstName && user.lastName) {
      setFirstName(user.firstName);
      setLastName(user.lastName);
    }
  }, [user]);

  const handleSubmit = (e) => {
    e.preventDefault();
    const paymentDetails = {
      cardNumber,
      expiryDate,
      cvv,
      firstName,
      lastName,
    };
    onSubmit(paymentDetails);
  };

  return (
    <form className="payment-form panel" onSubmit={handleSubmit}>
      <h2>Payment Information</h2>
      <div className="line-br"></div>

      <div className="form-group">
        <label htmlFor="cardNumber">Card Number</label>
        <input
          type="text"
          id="cardNumber"
          value={cardNumber}
          onChange={(e) => setCardNumber(e.target.value)}
          placeholder="1234 5678 9012 3456"
          required
        />
      </div>

      <div className="form-group">
        <label htmlFor="expiryDate">Expiry Date</label>
        <input
          type="text"
          id="expiryDate"
          value={expiryDate}
          onChange={(e) => setExpiryDate(e.target.value)}
          placeholder="MM/YY"
          required
        />
      </div>

      <div className="form-group">
        <label htmlFor="cvv">CVV</label>
        <input
          type="text"
          id="cvv"
          value={cvv}
          onChange={(e) => setCvv(e.target.value)}
          placeholder="123"
          required
        />
      </div>

      <div className="form-group">
        <label htmlFor="firstName">Cardholder First Name</label>
        <input
          type="text"
          id="firstName"
          value={firstName}
          onChange={(e) => setFirstName(e.target.value)}
          placeholder="John"
          required
        />
      </div>

      <div className="form-group">
        <label htmlFor="lastName">Cardholder Last Name</label>
        <input
          type="text"
          id="lastName"
          value={lastName}
          onChange={(e) => setLastName(e.target.value)}
          placeholder="Doe"
          required
        />
      </div>
      <div className="line-br"></div>
      <button type="submit" className="button payment-button">
        Confirm Payment
      </button>
    </form>
  );
}
