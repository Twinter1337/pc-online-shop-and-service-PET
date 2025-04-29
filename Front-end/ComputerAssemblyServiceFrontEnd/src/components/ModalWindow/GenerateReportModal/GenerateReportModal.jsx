import axios from "axios";
import { useState } from "react";
import "./GenerateReportModal.css";

export default function GenerateReportModal() {
  const [fromDate, setFromDate] = useState("");
  const [toDate, setToDate] = useState("");

  const handleGenerateReport = async () => {
    if (!fromDate || !toDate) {
      alert("Please select both dates.");
      return;
    }

    try {
      const response = await axios.get(
        `http://localhost:5153/api/Order/OrdersReport/pdf`,
        {
          params: { from: fromDate, to: toDate },
          responseType: "blob",
        }
      );

      const url = window.URL.createObjectURL(new Blob([response.data]));
      const a = document.createElement("a");
      a.href = url;
      a.download = `orders_report_${fromDate}_${toDate}.pdf`;
      document.body.appendChild(a);
      a.click();
      a.remove();
    } catch (err) {
      console.error("Failed to download report:", err);
      alert("Error generating report.");
    }
  };

  return (
    <section className="generate-report-modal">
      <h2>Generate Orders Report</h2>
      <div className="line-br"></div>
      <div className="form-group">
        <label htmlFor="fromDate">From:</label>
        <input
          type="date"
          id="fromDate"
          value={fromDate}
          onChange={(e) => setFromDate(e.target.value)}
        />
      </div>

      <div className="form-group">
        <label htmlFor="toDate">To:</label>
        <input
          type="date"
          id="toDate"
          value={toDate}
          onChange={(e) => setToDate(e.target.value)}
        />
      </div>
      <div className="line-br"></div>
      <button className="button" onClick={handleGenerateReport}>
        Generate Report
      </button>
    </section>
  );
}
