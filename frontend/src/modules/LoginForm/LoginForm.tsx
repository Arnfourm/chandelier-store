import "./LoginForm.css";
import { FormField } from "../../components/FormField/FormField";
import { FormButton } from "../../components/FormButton/FormButton";

export function LoginForm() {
    return (
        <div>
            <h1 className="text-stone-900 font-bold text-4xl">Войдите в аккаунт</h1>
            <p className="w-[630px] text-neutral-500 text-xl mt-[35px] mb-[60px]">
                Войдите в аккаунт, чтобы получить полный доступ к функционалу интернет-магазина
            </p>
            <FormField
                label={"Логин"}
                name={"email"}
                type={"email"}
                placeholder={"Введите ваш почтовый адрес"}
            />

            <a href="">Забыли пароль?</a>
            <FormButton name={"Войти"}></FormButton>
        </div>
    );
}
