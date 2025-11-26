import { Navigate } from "react-router-dom";
import { useAuth } from "./AuthContext";

export function PrivateRoute({ children, roles: allowedRoles = [] }) {
    const { isAuthenticated, roles, isInitializing } = useAuth();
    console.log("Проверка доступа:", { isAuthenticated, roles, allowedRoles, isInitializing });

    if (isInitializing) {
        return <div>Загрузка...</div>;
    }

    if (!isAuthenticated) {
        console.log("Неавторизован, редирект на /login");
        return <Navigate to="/login" replace />;
    }

    if (allowedRoles.length && !roles.some((r) => allowedRoles.includes(r))) {
        console.log("Нет роли для доступа, редирект на /login");
        return <Navigate to="/login" replace />;
    }

    return children;
}
