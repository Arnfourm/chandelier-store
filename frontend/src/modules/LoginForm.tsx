import { Link } from "react-router-dom";
import { FormField } from "../components/FormField";
import { FormButton } from "../components/FormButton";

export function LoginForm() {
    return (
        <form className="flex flex-col flex-1 items-center justify-center gap-y-[35px] text-center">
            <h1 className="text-4xl text-stone-900 font-bold">Войдите в аккаунт</h1>
            <p className="w-[630px] mb-[25px] text-xl text-neutral-500">
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

            <Link to="/login" className="-mt-5 ml-[-680px] text-x1 text-neutral-400 ">
                Забыли пароль?
            </Link>
            <FormButton name={"Войти"}></FormButton>
        </form>
    );
}
