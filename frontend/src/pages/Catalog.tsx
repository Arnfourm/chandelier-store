import { Footer } from "../modules/Footer";
import { Navbar } from "../modules/Navbar";

export function Catalog() {
    return (
        <>
            <Navbar />

            <div className="mt-10 mb-10 pl-[100px] pr-[100px] flex justify-between">
                <h1 className="uppercase text-5xl ">Каталог</h1>
                <p className="w-[200px] text-xl text-wrap">Мы сделаем вашу жизнь светлее!</p>
            </div>

            <Footer />
        </>
    );
}
