import "./StorePage.css";

import ProductCard from "../../components/ProductCard/ProductCard";
import { computersData } from "./computers-data.js";
import Page from "../Page/Page";

export default function StorePage() {
  return (
    <Page>
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
    </Page>
  );
}
