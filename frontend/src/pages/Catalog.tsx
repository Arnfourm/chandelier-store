import { Footer } from "../modules/Footer";
import { Navbar } from "../modules/Navbar";

export function Catalog() {
    return (
        <>
            <Navbar />
            <div className="flex justify-between mt-[40px] mb-[40px] pl-[100px] pr-[100px]">
                <h1 className="uppercase text-5xl ">Каталог</h1>
                <p className="w-[200px] text-wrap text-xl">Мы сделаем вашу жизнь светлее!</p>
            </div>

            <Footer />
        </>
    );
}
