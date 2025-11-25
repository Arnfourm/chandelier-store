import { Navigate } from "react-router-dom";
import { useAuth } from "../hooks/useAuth";
import type { ReactNode } from "react";

interface PrivateRouteProps {
    children: ReactNode;
    roles?: number[];
}

export function PrivateRoute({ children, roles }: PrivateRouteProps) {
    const { user, role } = useAuth();

    if (!user) return <Navigate to="/login" replace />;
    if (roles && !roles.includes(role!)) return <Navigate to="/" replace />;

    return <>{children}</>;
}
