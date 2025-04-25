import "./CartPage.css";

import Page from "../Page/Page";
import CartProductCard from "../../components/CartProductCard/CartProductCard";

export default function CartPage() {
  return (
    <Page className="cart-page">
      <section className="cart-page-container">
        <h1>Cart</h1>
        <div className="line-br"></div>
      </section>
      <section className="cart-items">
        {/* Cart items will be displayed here */}
        <CartProductCard
          imgUrl="https://it-blok.com.ua/image/cache/catalog/Korpusa/Gamemax%20Black%20Hole/1-110x110.png"
          title="Eco Build"
          price={10000}
          quantity={1}
        />
        <CartProductCard
          imgUrl="https://it-blok.com.ua/image/cache/catalog/Korpusa/Gamemax%20Black%20Hole/1-110x110.png"
          title="Eco Build"
          price={10000}
          quantity={1}
        />
        <CartProductCard
          imgUrl="https://it-blok.com.ua/image/cache/catalog/Korpusa/Gamemax%20Black%20Hole/1-110x110.png"
          title="Eco Build"
          price={10000}
          quantity={1}
        />
      </section>
    </Page>
  );
}
