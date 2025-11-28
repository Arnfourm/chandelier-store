import { Link } from "react-router-dom";
import { Logo } from "./Logo";
import { LoginButton } from "./LoginButton";
import { useAuth } from "../context/AuthContext";
import { ProfileButton } from "./UI-kit/ProfileButton";
import { CartButton } from "./UI-kit/CartButton";
import { Navbar } from "./UI-kit/Navbar";

export function Header() {
    const { isAuthenticated, user, role } = useAuth();

    return (
        <header className="h-20 flex items-center justify-between border-b-2 border-b-neutral-200">
            <Logo className="pl-[100px]" />
            <Navbar />
            <div className="flex">
                {isAuthenticated ? <CartButton /> : null}

                {isAuthenticated ? (
                    role === 1 ? (
                        <ProfileButton ref="/account" name={user?.name} />
                    ) : role === 2 || role === 3 ? (
                        <ProfileButton ref="/employee" name={user?.name} />
                    ) : null
                ) : (
                    <ProfileButton ref="/login" name="Войти" />
                )}
            </div>
        </header>
    );
}
