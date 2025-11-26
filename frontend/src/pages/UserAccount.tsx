import { Logo } from "../components/Logo";
import { useAuth } from "../auth/AuthContext";
import { useNavigate } from "react-router-dom";

export function UserAccount() {
    const { logout } = useAuth();
    const navigate = useNavigate();

    const handleLogout = () => {
        logout();
        navigate("/");
    };
    return (
        <>
            <Logo />
            <h1>Это страница личного кабинета пользователя!</h1>
            <button onClick={handleLogout}>Выйти</button>
        </>
    );
}
