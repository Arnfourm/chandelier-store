import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../auth/AuthContext";
import { AuthSwitchSection } from "../components/AuthSwitchSection";

export function Registration() {
    const [name, setName] = useState("");
    const [surname, setSurname] = useState("");
    const [birthday, setBirthday] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const [loading, setLoading] = useState(false);

    const auth = useAuth();
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);

        console.log("Отправка данных регистрации:", {
            name,
            surname,
            birthday,
            email,
            password,
        });

        try {
            const res = await fetch("http://localhost:9230/api/Users/Auth/SignUp", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    name,
                    surname,
                    birthday,
                    email,
                    password,
                }),
            });

            if (!res.ok) {
                console.warn("Ошибка регистрации", res.status);
                setLoading(false);
                return;
            }

            console.log("Регистрация успешна");

            const userRole = await auth.login(email, password);

            setLoading(false);

            if (userRole === 1) navigate("/account");
            else if (userRole === 2 || userRole === 3) navigate("/employee");
            else navigate("/");
        } catch (err) {
            console.error("Ошибка при регистрации:", err);
            setLoading(false);
        }
    };

    return (
        <div className="w-full h-full flex">
            <form
                onSubmit={handleSubmit}
                className="flex flex-col flex-1 items-center justify-center text-center gap-y-[35px]"
            >
                <h1 className="text-4xl text-stone-900 font-bold">Создайте новый аккаунт</h1>

                <div className="w-[800px] flex gap-x-4">
                    <input
                        name="name"
                        type="text"
                        placeholder="Введите ваше имя"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                        className="w-[300px]"
                    />

                    <input
                        name="surname"
                        type="text"
                        placeholder="Введите вашу фамилию"
                        value={surname}
                        onChange={(e) => setSurname(e.target.value)}
                        className="w-[500px]"
                    />
                </div>

                <input
                    name="birthday"
                    type="date"
                    value={birthday}
                    onChange={(e) => setBirthday(e.target.value)}
                    className="w-[800px]"
                />

                <input
                    name="email"
                    type="email"
                    placeholder="Введите ваш почтовый адрес"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    className="w-[800px]"
                />

                <input
                    name="password"
                    type="password"
                    placeholder="Введите ваш пароль"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    className="w-[800px]"
                />

                <button type="submit" disabled={loading}>
                    {loading ? "Создаём аккаунт..." : "Зарегистрироваться"}
                </button>
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
