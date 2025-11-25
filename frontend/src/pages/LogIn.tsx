import { useState, type ChangeEvent, type FormEvent } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../hooks/useAuth";
import { FormField } from "../components/FormField";
import { FormButton } from "../components/FormButton";
import { AuthSwitchSection } from "../modules/AuthSwitchSection";

export function LogIn() {
    const { login } = useAuth();
    const navigate = useNavigate();

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState<string | null>(null);

    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();
        setError(null);
        try {
            await login(email, password);

            const role = JSON.parse(localStorage.getItem("userRole") || "null");

            if (role === 1) navigate("/account");
            else if (role === 2 || role === 3) navigate("/employee");
            else navigate("/");
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

    const handleEmailChange = (e: ChangeEvent<HTMLInputElement>) => {
        setEmail(e.target.value);
    };

    const handlePasswordChange = (e: ChangeEvent<HTMLInputElement>) => {
        setPassword(e.target.value);
    };

    return (
        <div className="w-full h-full flex">
            <AuthSwitchSection
                headingName="Ещё нет аккаунта?"
                text="Создайте аккаунт, чтобы получить полный доступ к вашим заказам и отзывам"
                buttonName="Зарегистрироваться"
                buttonRef="/reg"
                headerClassName="ml-[80px] text-neutral-500"
            />

            <form
                className="flex flex-col flex-1 items-center justify-center gap-y-[35px] text-center"
                onSubmit={handleSubmit}
            >
                <h1 className="text-4xl text-stone-900 font-bold">Войдите в аккаунт</h1>
                <p className="w-[630px] mb-[25px] text-xl text-neutral-500">
                    Войдите в аккаунт, чтобы получить полный доступ к функционалу интернет-магазина
                </p>

                {error && <p className="text-red-500">{error}</p>}

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

                <FormButton name="Войти" type="submit" />
            </form>
        </div>
    );
}
