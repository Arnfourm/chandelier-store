import { FooterLink } from "./FooterLink";

export function ForClientsSection() {
    return (
        <div className="flex flex-col gap-3">
            <h5 className="mb-[15px] text-xl font-semibold text-neutral-200">Клиентам</h5>
            <FooterLink name={"Каталог"} ref={"/catalog"} />
            <FooterLink name={"Отзывы"} ref={"/"} />
        </div>
    );
}
