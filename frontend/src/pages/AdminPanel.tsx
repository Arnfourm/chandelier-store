import { Logo } from "../components/Logo";
import { useAuth } from "../context/AuthContext";
import { useNavigate } from "react-router-dom";

export function AdminPanel() {
    const { user, logout } = useAuth();
    const navigate = useNavigate();

    const handleLogout = () => {
        logout();
        navigate("/");
    };
    return (
        <>
            <Logo />
            <h1>Это страница "Админка" для сотрудников и админа</h1>
            <h2>Привет, {user?.name}!!</h2>
            <button onClick={handleLogout}>Выйти</button>
        </>
    );
}
