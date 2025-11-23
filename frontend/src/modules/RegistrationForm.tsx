import { FormField } from "../components/FormField";
import { FormButton } from "../components/FormButton";

export function RegistrationForm() {
    return (
        <form className="flex flex-col flex-1 items-center justify-center text-center gap-y-[35px]">
            <h1 className="text-4xl text-stone-900 font-bold">Создайте новый аккаунт</h1>

            <div className="w-[800px] flex">
                <FormField
                    label={"Имя"}
                    name={"name"}
                    type={"text"}
                    placeholder={"Введите ваше имя"}
                    className="w-[300px]"
                />

                <FormField
                    label={"Фамилия"}
                    name={"surname"}
                    type={"text"}
                    placeholder={"Введите вашу фамилию"}
                    className="w-[500px]"
                />
            </div>

            <FormField
                label={"Дата рождения"}
                name={"birthday"}
                type={"date"}
                placeholder={"Введите вашу дату рождения"}
                className="w-[800px]"
            />

            <FormField
                label={"Email"}
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

            <FormButton name={"Зарегистрироваться"}></FormButton>
        </form>
    );
}
