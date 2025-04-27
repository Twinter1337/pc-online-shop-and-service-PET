import "./SearchInput.css";

export default function SearchInput({
  value,
  onChange,
  placeholder = "Search...",
  searchImg,
}) {
  return (
    <div className="search-input-wrapper">
      <img src={searchImg} alt="search icon" className="search-img" />
      <input
        type="text"
        className="search-input"
        value={value}
        onChange={onChange}
        placeholder={placeholder}
      />
    </div>
  );
}
