import { Link } from "react-router-dom";
import { HomeButton } from "../components/HomeButton";
import { Footer } from "../modules/Footer";
import { Navbar } from "../modules/Navbar";

export function Home() {
    return (
        <>
            <Navbar />
            <div className="h-[600px] bg-neutral-200">
                <h1>Это страница "Главная"</h1>
                <HomeButton name={"Перейти в каталог"} ref={"/catalog"} />
            </div>
            <div className="text-center flex flex-col items-center mt-[3%] mb-[3%]">
                <h2 className="uppercase font-bold text-5xl w-[74%] mb-[2%]">
                    <span className="text-amber-500">Империя люстр</span> – российская компания по
                    продаже светодиодного оборудования, эксперт в области интерьерного и уличного
                    освещения
                </h2>
                <HomeButton name={"О компании"} ref={"/contacts"} />
            </div>
            <Footer />
        </>
    );
}
