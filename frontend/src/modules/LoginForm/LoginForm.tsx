import "./LoginForm.css";
import { FormField } from "../../components/FormField/FormField";
import { FormButton } from "../../components/FormButton/FormButton";

export function LoginForm() {
    return (
        <div className="flex flex-col flex-1 items-center justify-center text-center gap-y-[35px]">
            <h1 className="text-stone-900 font-bold text-4xl">Войдите в аккаунт</h1>
            <p className="w-[630px] text-neutral-500 text-xl mb-[25px]">
                Войдите в аккаунт, чтобы получить полный доступ к функционалу интернет-магазина
            </p>

            <FormField
                label={"Логин"}
                name={"email"}
                type={"email"}
                placeholder={"Введите ваш почтовый адрес"}
            />

            <FormField
                label={"Пароль"}
                name={"password"}
                type={"password"}
                placeholder={"Введите ваш пароль"}
            />

            <a href="" className="mt-[-20px] ml-[-680px] text-x1 text-neutral-400 ">
                Забыли пароль?
            </a>
            <FormButton name={"Войти"}></FormButton>
        </div>
    );
}
