import { Link } from "react-router-dom";
import { Logo } from "../components/Logo";
import { LoginButton } from "../components/LoginButton";

export function Navbar() {
    return (
        <nav className="h-20 flex items-center justify-between border-b-2 border-b-neutral-200">
            <Logo className="pl-[100px]" />

            <ul className="w-[320px] ml-[-100px] flex justify-between self-center text-xl">
                <li>
                    <Link to="/">Главная</Link>
                </li>

                <li>
                    <Link to="/catalog">Каталог</Link>
                </li>

                <li>
                    <Link to="/contacts">Контакты</Link>
                </li>
            </ul>

            <LoginButton name="Войти" ref="/login" />
        </nav>
    );
}
