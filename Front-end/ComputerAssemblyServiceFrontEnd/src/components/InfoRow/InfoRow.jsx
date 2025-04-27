// InfoRow.jsx
const InfoRow = ({ label, value }) => {
  return (
    <div style={{ display: "flex", marginBottom: "10px" }}>
      <strong style={{ width: "100px" }}>{label}:</strong>
      <span>{value}</span>
    </div>
  );
};

export default InfoRow;
