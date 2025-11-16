import { Footer } from "../modules/Footer";
import { Navbar } from "../modules/Navbar";
import { Promo } from "../modules/Promo";
import { Slider } from "../modules/Slider";

export function Home() {
    return (
        <>
            <Navbar />
            <Slider />
            <Promo />
            <Footer />
        </>
    );
}
