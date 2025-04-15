import "./StorePage.css";

import StoreFilter from "./StoreFilter/StoreFilter";
import ProductCard from "../../components/ProductCard/ProductCard";
import { computersData } from "./computers-data.js";
import Page from "../Page/Page";
import ScrollToTopButton from "../../components/ScrollToTopButton/ScrolToTopButton.jsx";

export default function StorePage() {
  return (
    <Page>
      <StoreFilter />
      <div class="products">
        {computersData.map((comp, index) => (
          <ProductCard
            key={index}
            title={comp.title}
            imageUrl={comp.img}
            processor={comp.cpu}
            videoCard={comp.gpu}
            memoryType={comp.memoryType}
            ram={comp.ram}
            storage={comp.ssd}
            price={comp.price}
          />
        ))}
      </div>
      <ScrollToTopButton />
    </Page>
  );
}
