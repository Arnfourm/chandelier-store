import { HomeButton } from "./HomeButton";

export function Promo() {
    return (
        <div className="mt-[4%] mb-[6%] flex flex-col items-center text-center">
            <h2 className="w-[74%] mb-[2%] leading-[1.4] text-5xl font-semibold uppercase">
                <span className="text-amber-500">Империя люстр</span> – российская компания по
                продаже светодиодного оборудования, эксперт в области интерьерного и уличного
                освещения
            </h2>
            <HomeButton name={"О компании"} ref={"/contacts"} />
        </div>
    );
}
