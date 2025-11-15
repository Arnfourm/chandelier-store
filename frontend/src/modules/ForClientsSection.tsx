import { FooterLink } from "../components/FooterLink";

export function ForClientsSection() {
    return (
        <div className="flex flex-col gap-3">
            <h5 className="text-neutral-200 font-semibold text-xl mb-[15px]">Клиентам</h5>
            <FooterLink name={"Каталог"} ref={"/catalog"} />
            <FooterLink name={"Отзывы"} ref={"/"} />
        </div>
    );
}
