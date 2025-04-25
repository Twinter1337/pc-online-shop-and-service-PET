import "./StorePage.css";

import StoreFilter from "./StoreFilter/StoreFilter";
import ProductCard from "../../components/ProductCard/ProductCard";
import CartProductCard from "../../components/CartProductCard/CartProductCard";

import * as productCrudService from "../../scripts/crud-services/product-crud-service.js";
import { getProductComponentModel } from "../../scripts/services/product-service.js";
import Page from "../Page/Page";
import ScrollToTopButton from "../../components/ScrollToTopButton/ScrolToTopButton.jsx";
import { useEffect, useState } from "react";

export default function StorePage() {
  const [productsData, setProductsData] = useState([]);

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

  return (
    <Page className="page">
      {/* <CartProductCard
        imgUrl="https://it-blok.com.ua/image/cache/catalog/Korpusa/Deepcool%20MATREXX%2030/1-367x367.png.pagespeed.ce.a2tK4-D_WV.png"
        title="Eco Build"
        price={100000}
        quantity={1}
      /> */}
      <div class="products">
        {productsData.map((product, index) => (
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
          />
        ))}
      </div>
      <ScrollToTopButton />
    </Page>
  );
}
