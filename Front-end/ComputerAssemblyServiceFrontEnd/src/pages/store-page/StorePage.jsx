import "./StorePage.css";

import StoreFilter from "./StoreFilter/StoreFilter";
import ProductCard from "../../components/ProductCard/ProductCard";
import SearchInput from "../../components/SearchInput/SearchInput.jsx";
import ScrollToTopButton from "../../components/ScrollToTopButton/ScrolToTopButton.jsx";

import * as productCrudService from "../../scripts/crud-services/product-crud-service.js";
import { getProductComponentModel } from "../../scripts/services/product-service.js";
import Page from "../Page/Page";
import { useEffect, useState } from "react";

import searchImg from "../../assets/SearchPng/search-50.png";

export default function StorePage() {
  const [productsData, setProductsData] = useState([]);
  const [searchQuery, setSearchQuery] = useState("");
  const [currentPage, setCurrentPage] = useState(1);
  const [filters, setFilters] = useState({
    minPrice: "",
    maxPrice: "",
    productType: "",
    selectedComponents: [],
  });

  const PRODUCTS_PER_PAGE = 16;

  useEffect(() => {
    productCrudService
      .getAllProducts()
      .then((data) => {
        setProductsData(data);
      })
      .catch((error) => {
        console.error("Error fetching products:", error);
      });
  }, []);

  const filteredProducts = productsData.filter((product) => {
    const { minPrice, maxPrice, productType, selectedComponents } = filters;

    const matchesSearch = product.prebuildPattern.prebuildName
      .toLowerCase()
      .includes(searchQuery.toLowerCase());

    const price = product.prebuildPattern.basePrice;
    const matchesPrice =
      (!minPrice || price >= Number(minPrice)) &&
      (!maxPrice || price <= Number(maxPrice));

    const matchesProductType = !productType || product.category === productType;

    const componentIds = product.prebuildPattern.components.map(
      (c) => c.componentId
    );
    const matchesComponents = selectedComponents.every((id) =>
      componentIds.includes(id)
    );

    return (
      matchesSearch && matchesPrice && matchesProductType && matchesComponents
    );
  });

  const totalPages = Math.ceil(filteredProducts.length / PRODUCTS_PER_PAGE);

  const displayedProducts = filteredProducts.slice(
    (currentPage - 1) * PRODUCTS_PER_PAGE,
    currentPage * PRODUCTS_PER_PAGE
  );

  const handlePageChange = (pageNumber) => {
    setCurrentPage(pageNumber);
    window.scrollTo({ top: 0, behavior: "smooth" }); // Плавно наверх при зміні сторінки
  };

  return (
    <Page className="store-page">
      <div className="heading-title">
        <h1>Store</h1>
        <div className="line-br"></div>
      </div>

      <div className="search-input-store">
        <SearchInput
          value={searchQuery}
          onChange={(e) => {
            setSearchQuery(e.target.value);
            setCurrentPage(1);
          }}
          searchImg={searchImg}
          placeholder="Search builds..."
        />
      </div>

      <StoreFilter
        onFilterChange={(newFilters) => {
          setFilters(newFilters);
          setCurrentPage(1);
        }}
      />

      <div className="products">
        {productsData.length > 0 ? (
          displayedProducts.map((product, index) => (
            <ProductCard
              key={index}
              title={product.prebuildPattern.prebuildName}
              imageUrl={product.imgUrl}
              processor={getProductComponentModel(
                "CPU",
                product.prebuildPattern.components
              )}
              videoCard={getProductComponentModel(
                "GPU",
                product.prebuildPattern.components
              )}
              ram={getProductComponentModel(
                "RAM",
                product.prebuildPattern.components
              )}
              storage={
                getProductComponentModel(
                  "HDD",
                  product.prebuildPattern.components
                ) +
                "/" +
                getProductComponentModel(
                  "SSD",
                  product.prebuildPattern.components
                )
              }
              price={product.prebuildPattern.basePrice}
              productId={product.sku}
            />
          ))
        ) : (
          <p>Loading store...</p>
        )}
      </div>

      {totalPages > 1 && (
        <div className="pagination">
          <button
            onClick={() => handlePageChange(currentPage - 1)}
            disabled={currentPage === 1}
          >
            ❮
          </button>

          {currentPage > 3 && (
            <>
              <button onClick={() => handlePageChange(1)}>1</button>
              {currentPage > 4 && <span className="dots">...</span>}
            </>
          )}
          {Array.from({ length: 2 }, (_, i) => currentPage - 2 + i)
            .filter((page) => page > 1 && page < currentPage)
            .map((page) => (
              <button key={page} onClick={() => handlePageChange(page)}>
                {page}
              </button>
            ))}
          <button className="active">{currentPage}</button>
          {Array.from({ length: 2 }, (_, i) => currentPage + i + 1)
            .filter((page) => page < totalPages)
            .map((page) => (
              <button key={page} onClick={() => handlePageChange(page)}>
                {page}
              </button>
            ))}
          {currentPage < totalPages - 2 && (
            <>
              {currentPage < totalPages - 3 && (
                <span className="dots">...</span>
              )}
              <button onClick={() => handlePageChange(totalPages)}>
                {totalPages}
              </button>
            </>
          )}
          <button
            onClick={() => handlePageChange(currentPage + 1)}
            disabled={currentPage === totalPages}
          >
            ❯
          </button>
        </div>
      )}

      <ScrollToTopButton />
    </Page>
  );
}
