import { HomeButton } from "../components/HomeButton";

export function Promo() {
    return (
        <div className="text-center flex flex-col items-center mt-[4%] mb-[6%]">
            <h2 className="uppercase font-semibold text-5xl w-[74%] mb-[2%] leading-[1.4]">
                <span className="text-amber-500">Империя люстр</span> – российская компания по
                продаже светодиодного оборудования, эксперт в области интерьерного и уличного
                освещения
            </h2>
            <HomeButton name={"О компании"} ref={"/contacts"} />
        </div>
    );
}
