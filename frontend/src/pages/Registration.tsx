import { useState, type ChangeEvent, type FormEvent } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../hooks/useAuth";
import { FormField } from "../components/FormField";
import { FormButton } from "../components/FormButton";
import { AuthSwitchSection } from "../modules/AuthSwitchSection";

export function Registration() {
    const { register } = useAuth();
    const navigate = useNavigate();

    const [name, setName] = useState("");
    const [surname, setSurname] = useState("");
    const [birthday, setBirthday] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState<string | null>(null);

    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();
        setError(null);

        try {
            await register({
                name,
                surname,
                birthday,
                email,
                password,
                userRole: 1,
            });

            navigate("/account");
        } catch (err: unknown) {
            if (err instanceof Error) {
                setError(err.message);
            } else if (typeof err === "object" && err !== null && "response" in err) {
                const axiosError = err as { response?: { data?: string } };
                setError(axiosError.response?.data || "Ошибка входа");
            } else {
                setError("Произошла неизвестная ошибка");
            }
        }
    };

    const handleNameChange = (e: ChangeEvent<HTMLInputElement>) => {
        setName(e.target.value);
    };

    const handleSurnameChange = (e: ChangeEvent<HTMLInputElement>) => {
        setSurname(e.target.value);
    };

    const handleBirthdayChange = (e: ChangeEvent<HTMLInputElement>) => {
        setBirthday(e.target.value);
    };

    const handleEmailChange = (e: ChangeEvent<HTMLInputElement>) => {
        setEmail(e.target.value);
    };

    const handlePasswordChange = (e: ChangeEvent<HTMLInputElement>) => {
        setPassword(e.target.value);
    };

    return (
        <div className="w-full h-full flex">
            <form
                className="flex flex-col flex-1 items-center justify-center text-center gap-y-[35px]"
                onSubmit={handleSubmit}
            >
                <h1 className="text-4xl text-stone-900 font-bold">Создайте новый аккаунт</h1>

                {error && <p className="text-red-500">{error}</p>}

                <div className="w-[800px] flex gap-x-4">
                    <FormField
                        label="Имя"
                        name="name"
                        type="text"
                        placeholder="Введите ваше имя"
                        value={name}
                        onChange={handleNameChange}
                        className="w-[300px]"
                    />
                    <FormField
                        label="Фамилия"
                        name="surname"
                        type="text"
                        placeholder="Введите вашу фамилию"
                        value={surname}
                        onChange={handleSurnameChange}
                        className="w-[500px]"
                    />
                </div>

                <FormField
                    label="Дата рождения"
                    name="birthday"
                    type="date"
                    placeholder="Введите вашу дату рождения"
                    value={birthday}
                    onChange={handleBirthdayChange}
                    className="w-[800px]"
                />

                <FormField
                    label="Email"
                    name="email"
                    type="email"
                    placeholder="Введите ваш почтовый адрес"
                    value={email}
                    onChange={handleEmailChange}
                    className="w-[800px]"
                />

                <FormField
                    label="Пароль"
                    name="password"
                    type="password"
                    placeholder="Введите ваш пароль"
                    value={password}
                    onChange={handlePasswordChange}
                    className="w-[800px]"
                />

                <FormButton name="Зарегистрироваться" type="submit" />
            </form>

            <AuthSwitchSection
                headingName="Уже есть аккаунт?"
                text="Войдите в аккаунт, чтобы получить полный доступ к функционалу интернет-магазина!"
                buttonName="Войти"
                buttonRef="/login"
                headerClassName="mr-[80px] text-right text-neutral-500"
            />
        </div>
    );
}
