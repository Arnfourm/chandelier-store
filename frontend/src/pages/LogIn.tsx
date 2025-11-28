import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";
import { AuthSwitchSection } from "../components/AuthSwitchSection";
import { FormField } from "../components/UI-kit/FormField";
import { FormButton } from "../components/UI-kit/FormButton";

// Тестовый клиент: test@example.com, password123
// Тестовый работник: empl@example.com, work111
// Тестовый админ: admin@example.com, 54321

export function LogIn() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [loading, setLoading] = useState(false);

    const auth = useAuth();
    const navigate = useNavigate();

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        console.log("Отправляем данные формы для логина:", { email, password });
        setLoading(true);

        const userRole = await auth.login(email, password);
        setLoading(false);

        if (userRole !== null) {
            console.log("Логин успешен. Роль пользователя:", userRole);

            if (userRole === 1) navigate("/account");
            else if (userRole === 2 || userRole === 3) navigate("/employee");
            else navigate("/");
        } else {
            console.warn("Не удалось войти. Проверьте логин/пароль");
        }
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
                onSubmit={handleSubmit}
                className="flex flex-col flex-1 items-center justify-center gap-y-[35px] text-center"
            >
                <h1 className="text-4xl text-stone-900 font-bold">Войдите в аккаунт</h1>
                <p className="w-[630px] mb-[25px] text-xl text-neutral-500">
                    Войдите в аккаунт, чтобы получить полный доступ к функционалу интернет-магазина
                </p>

                <FormField
                    label="Логин"
                    name="email"
                    type="email"
                    placeholder="Введите ваш почтовый адрес"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    className="w-[800px]"
                />

                <FormField
                    label="Пароль"
                    name="password"
                    type="password"
                    placeholder="Введите ваш пароль"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    className="w-[800px]"
                />

                <FormButton
                    type="submit"
                    name={!loading ? "Войти" : "Входим..."}
                    disabled={loading}
                />
            </form>
        </div>
    );
}
