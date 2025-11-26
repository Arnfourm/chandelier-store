import { FooterContact } from "./FooterContact";

export function ContactsSection() {
    return (
        <div className="flex flex-col gap-y-5">
            <FooterContact label={"Телефон"} info={"+7 (910) 888-88-88"} />
            <FooterContact label={"Почта"} info={"INFO@YANDEX.RU"} />
        </div>
    );
}
