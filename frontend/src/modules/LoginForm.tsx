import { Link } from "react-router-dom";
import { FormField } from "../components/FormField";
import { FormButton } from "../components/FormButton";

export function LoginForm() {
    return (
        <form className="flex flex-col flex-1 items-center justify-center text-center gap-y-[35px]">
            <h1 className="text-stone-900 font-bold text-4xl">Войдите в аккаунт</h1>
            <p className="w-[630px] text-neutral-500 text-xl mb-[25px]">
                Войдите в аккаунт, чтобы получить полный доступ к функционалу интернет-магазина
            </p>

            <FormField
                label={"Логин"}
                name={"email"}
                type={"email"}
                placeholder={"Введите ваш почтовый адрес"}
                className="w-[800px]"
            />

            <FormField
                label={"Пароль"}
                name={"password"}
                type={"password"}
                placeholder={"Введите ваш пароль"}
                className="w-[800px]"
            />

            <Link to="/login" className="mt-[-20px] ml-[-680px] text-x1 text-neutral-400 ">
                Забыли пароль?
            </Link>
            <FormButton name={"Войти"}></FormButton>
        </form>
    );
}
