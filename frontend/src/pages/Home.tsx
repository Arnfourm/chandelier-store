import { Footer } from "../components/Footer";
import { Navbar } from "../components/Navbar";
import { Promo } from "../components/Promo";
import { Slider } from "../components/Slider";

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
