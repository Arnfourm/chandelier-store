import { Link } from "react-router-dom";
import { Logo } from "./Logo";
import { LoginButton } from "./LoginButton";
import { useAuth } from "../context/AuthContext";

export function Navbar() {
    const { isAuthenticated, user, role } = useAuth();

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
            {isAuthenticated && (
                <button>
                    <img src="icons/cart-icon.png" alt="Cart icon" />
                </button>
            )}

            {isAuthenticated ? (
                role === 1 ? (
                    <Link to="/account">{user?.name}</Link>
                ) : role === 2 || role === 3 ? (
                    <Link to="/employee">{user?.name}</Link>
                ) : null
            ) : (
                <Link to="/login">Войти</Link>
            )}
        </nav>
    );
}
