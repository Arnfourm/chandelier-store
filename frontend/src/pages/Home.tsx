import { Footer } from "../modules/Footer";
import { Navbar } from "../modules/Navbar";

export function Home() {
    return (
        <>
            <Navbar />
            <h1>Это страница "Главная"</h1>
            <Footer />
        </>
    );
}
