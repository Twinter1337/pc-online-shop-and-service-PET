import "./StorePage.css";

import StoreFilter from "./StoreFilter/StoreFilter";
import ProductCard from "../../components/ProductCard/ProductCard";
import CartProductCard from "../../components/CartProductCard/CartProductCard";
import SearchInput from "../../components/SearchInput/SearchInput.jsx"; // Імпортуємо наш пошук

import * as productCrudService from "../../scripts/crud-services/product-crud-service.js";
import { getProductComponentModel } from "../../scripts/services/product-service.js";
import Page from "../Page/Page";
import ScrollToTopButton from "../../components/ScrollToTopButton/ScrolToTopButton.jsx";
import { useEffect, useState } from "react";

import searchImg from "../../assets/SearchPng/search-50.png"; // Путь до картинки пошуку
import { p } from "framer-motion/client";

export default function StorePage() {
  const [productsData, setProductsData] = useState([]);
  const [searchQuery, setSearchQuery] = useState("");

  useEffect(() => {
    productCrudService
      .getAllProducts()
      .then((data) => {
        setProductsData(data);
        console.log(data);
      })
      .catch((error) => {
        console.error("Error fetching products:", error);
      });
  }, []);

  const filteredProducts = productsData.filter((product) =>
    product.prebuildPattern.prebuildName
      .toLowerCase()
      .includes(searchQuery.toLowerCase())
  );

  return (
    <Page className="store-page">
      <div className="heading-title">
        <h1>Store</h1>
        <div className="line-br"></div>
      </div>
      <div className="search-input-store">
        <SearchInput
          value={searchQuery}
          onChange={(e) => setSearchQuery(e.target.value)}
          searchImg={searchImg}
          placeholder="Search builds..."
        />
      </div>

      <div className="products">
        {productsData.length > 0 ? (
          filteredProducts.map((product, index) => (
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

      <ScrollToTopButton />
    </Page>
  );
}
