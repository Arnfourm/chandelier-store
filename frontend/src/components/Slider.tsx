import { HomeButton } from "./HomeButton";

export function Slider() {
    return (
        <div className="h-[600px] pl-[100px] pr-[200px] flex justify-between bg-neutral-200">
            <div>
                <img
                    src="images/chandelier-home.png"
                    className="h-[95%]"
                    alt="Chandelier image"
                ></img>
            </div>

            <div className="h-full flex flex-col items-center justify-center gap-y-10 text-center">
                <h1 className="leading-[1.2] text-6xl font-bold uppercase">
                    Дизайнерские
                    <br />
                    светильники
                </h1>

                <p className="w-[800px] mb-[30px] text-xl text-neutral-600">
                    Продаем современное высококачественное оборудование и отдельные комплектующие
                    для освещения интерьеров жилых и коммерческих помещений
                </p>

                <HomeButton name={"Перейти в каталог"} ref={"/catalog"} />
            </div>
        </div>
    );
}
