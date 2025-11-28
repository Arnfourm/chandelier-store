import { NavLink } from "react-router-dom";

export function Navbar() {
    return (
        <nav>
            <ul className="w-[320px] -ml-20 flex justify-between self-center text-xl">
                <li>
                    <NavLink
                        to="/"
                        className={({ isActive }) =>
                            `transition-all ${
                                isActive
                                    ? "underline underline-offset-10 decoration-3 decoration-amber-500"
                                    : "no-underline hover:text-gray-500"
                            }`
                        }
                    >
                        Главная
                    </NavLink>
                </li>
                <li>
                    <NavLink
                        to="/catalog"
                        className={({ isActive }) =>
                            `transition-all ${
                                isActive
                                    ? "underline underline-offset-10 decoration-3 decoration-amber-500"
                                    : "no-underline hover:text-gray-500"
                            }`
                        }
                    >
                        Каталог
                    </NavLink>
                </li>
                <li>
                    <NavLink
                        to="/contacts"
                        className={({ isActive }) =>
                            `transition-all ${
                                isActive
                                    ? "underline underline-offset-10 decoration-3 decoration-amber-500"
                                    : "no-underline hover:text-gray-500"
                            }`
                        }
                    >
                        Контакты
                    </NavLink>
                </li>
            </ul>
        </nav>
    );
}
