import { FooterLink } from "./FooterLink";

export function AboutSection() {
    return (
        <div className="flex flex-col gap-3">
            <h5 className="mb-[15px] text-xl text-neutral-200 font-semibold">О компании</h5>
            <FooterLink name={"Адрес"} ref={"/contacts"} />
            <FooterLink name={"Контакты"} ref={"/contacts"} />
        </div>
    );
}
