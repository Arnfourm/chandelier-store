// Разрешает доступ к странице только пользователю с определенной ролью

import { Navigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";
import { type ReactNode } from "react";

interface PrivateRouteProps {
    children: ReactNode;
    allowedRoles?: number[];
}

export function PrivateRoute({ children, allowedRoles = [] }: PrivateRouteProps) {
    const { isAuthenticated, role, isInitializing } = useAuth();

    if (isInitializing) {
        return <div>Загрузка...</div>;
    }

    if (!isAuthenticated) {
        console.log("Неавторизован, редирект на /login");
        return <Navigate to="/login" replace />;
    }

    if (allowedRoles.length && role !== null && !allowedRoles.includes(role)) {
        console.log("Нет роли для доступа, редирект на /login");
        return <Navigate to="/login" replace />;
    }

    return children;
}
