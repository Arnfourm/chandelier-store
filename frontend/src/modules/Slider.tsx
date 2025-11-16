import { HomeButton } from "../components/HomeButton";

export function Slider() {
    return (
        <div className="h-[600px] bg-neutral-200 flex justify-between pl-[100px] pr-[200px]">
            <div>
                <img src="images/chandelier-home.png" className="h-[95%]"></img>
            </div>
            <div className="h-full flex flex-col items-center justify-center gap-y-10 text-center ">
                <h1 className="uppercase font-bold text-6xl leading-[1.2]">
                    Дизайнерские
                    <br />
                    светильники
                </h1>
                <p className="w-[800px] text-xl text-neutral-600 mb-[30px]">
                    Продаем современное высококачественное оборудование и отдельные комплектующие
                    для освещения интерьеров жилых и коммерческих помещений
                </p>
                <HomeButton name={"Перейти в каталог"} ref={"/catalog"} />
            </div>
        </div>
    );
}
