import { Footer } from "../components/Footer";
import { Header } from "../components/Header";

export function Catalog() {
    return (
        <>
            <Header />

            <div className="mt-10 mb-10 pl-[100px] pr-[100px] flex justify-between">
                <h1 className="uppercase text-5xl ">Каталог</h1>
                <p className="w-[200px] text-xl text-wrap">Мы сделаем вашу жизнь светлее!</p>
            </div>

            <Footer />
        </>
    );
}
