import "./ContactPage.css";
import Page from "../Page/Page";
import { useState } from "react";

export default function ContactPage() {
  const [isLoaded, setIsLoaded] = useState(false);

  return (
    <Page className="contact-us-page">
      <h1>Contacts</h1>
      <div className="line-br"></div>

      <div className="contacts">
        <div>
          <p className="title">Service center/Shop</p>
          <strong>Address:</strong> Lukasha 4V, Lviv, Ukraine
        </div>
        <p>
          Mon.-Fri. from 09:00 to 19:00 <br />
          Sat. from 10:00 to 18:00
        </p>
        <p>
          <strong>Phone number:</strong> (098) 172 28 08
        </p>
        <p>
          <strong>Email:</strong> BeePC@gmail.com
        </p>
      </div>

      <div className="map-container">
        {!isLoaded && <div className="map-skeleton">Loading map...</div>}

        <iframe
          className={`map ${isLoaded ? "visible" : "hidden"}`}
          src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2033.4395448252683!2d24.015735362428675!3d49.82668036686142!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x473ae787ef4c433b%3A0xf427ae70c058c286!2zNFYsINCy0YPQu9C40YbRjyDQm9GD0LrQsNGI0LAsIDTQkiwg0JvRjNCy0ZbQsiwg0JvRjNCy0ZbQstGB0YzQutCwINC-0LHQu9Cw0YHRgtGMLCA3OTAwMA!5e0!3m2!1suk!2sua!4v1744721590736!5m2!1suk!2sua"
          width="800"
          height="450"
          style={{ border: 0 }}
          allowFullScreen=""
          loading="lazy"
          referrerPolicy="no-referrer-when-downgrade"
          onLoad={() => setIsLoaded(true)}
        ></iframe>
      </div>
    </Page>
  );
}
