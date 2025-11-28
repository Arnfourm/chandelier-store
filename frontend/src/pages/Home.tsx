import { Footer } from "../components/Footer";
import { Header } from "../components/Header";
import { Promo } from "../components/Promo";
import { Slider } from "../components/Slider";

export function Home() {
    return (
        <>
            <Header />
            <Slider />
            <Promo />
            <Footer />
        </>
    );
}
