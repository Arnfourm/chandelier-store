// Разрешает доступ к странице только пользователю с определенной ролью

import { Navigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";

export function PrivateRoute({ children, allowedRoles = [] }) {
    const { isAuthenticated, role, isInitializing } = useAuth();

    if (isInitializing) {
        return <div>Загрузка...</div>;
    }

    if (!isAuthenticated) {
        console.log("Неавторизован, редирект на /login");
        return <Navigate to="/login" replace />;
    }

    if (allowedRoles.length && !allowedRoles.includes(role)) {
        console.log("Нет роли для доступа, редирект на /login");
        return <Navigate to="/login" replace />;
    }

    return children;
}
