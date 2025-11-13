import "./PromoSection.css";
import { Logo } from "../../components/Logo/Logo";
import { Button } from "../../components/Button/Button";

export function PromoSection() {
    return (
        <div className="h-full w-[35%] bg-stone-900 pt-[35px] flex flex-col">
            <Logo />

            <div className="flex-1 flex flex-col items-center text-center mt-[315px]">
                <h2 className="text-neutral-200 font-semibold text-4xl">Ещё нет аккаунта?</h2>
                <p className="w-[500px] text-neutral-500 text-xl mt-[35px] mb-[60px]">
                    Создайте аккаунт, чтобы получить полный доступ к вашим заказам и отзывам
                </p>
                <Button name={"Зарегистрироваться"}></Button>
            </div>
        </div>
    );
}
