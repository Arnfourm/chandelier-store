import { Logo } from "../components/Logo";
import { LoginButton } from "../components/LoginButton";

export function Navbar() {
    return (
        <nav className="h-[80px] flex items-center justify-between border-b-2 border-b-neutral-200">
            <Logo className="pl-[100px]" />

            <ul className="w-[320px] ml-[-100px] self-center flex justify-between text-xl">
                <li>
                    <a href="">Главная</a>
                </li>
                <li>
                    <a href="">Каталог</a>
                </li>
                <li>
                    <a href="">Контакты</a>
                </li>
            </ul>

            <LoginButton />
        </nav>
    );
}
