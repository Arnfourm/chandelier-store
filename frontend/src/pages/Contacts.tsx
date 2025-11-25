import { Contact } from "../components/Contact";
import { Footer } from "../modules/Footer";
import { Navbar } from "../modules/Navbar";

export function Contacts() {
    return (
        <>
            <Navbar />
            <h1 className="uppercase ml-[100px] mt-10 text-5xl">Контакты</h1>
            <div className="pl-[400px] pr-[400px] flex justify-between">
                <Contact label={"Телефон"} info={"+7 (910) 888-88-88"} />
                <Contact label={"Электронная почта"} info={"info@yandex.ru"} />
                <Contact
                    label={"Центральный офис"}
                    info={"г. Владимир, ул. Студеная гора, д. 34 (2 этаж)"}
                />
            </div>

            <iframe
                src="https://yandex.ru/map-widget/v1/?um=constructor%3A56698f229f8cfe4e13f2c8d541d5fa0e1354dcad996f06fb3cb6a5e772ca9d10&amp;source=constructor"
                className="w-full h-[700px]"
            ></iframe>
            <Footer />
        </>
    );
}
