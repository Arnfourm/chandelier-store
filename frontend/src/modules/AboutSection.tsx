import { FooterLink } from "../components/FooterLink";

export function AboutSection() {
    return (
        <div className="flex flex-col gap-3">
            <h5 className="text-neutral-200 font-semibold text-xl mb-[15px]">О компании</h5>
            <FooterLink name={"Адрес"} ref={"/contacts"} />
            <FooterLink name={"Контакты"} ref={"/contacts"} />
        </div>
    );
}
