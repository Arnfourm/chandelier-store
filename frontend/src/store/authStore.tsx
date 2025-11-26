import { useState, type ReactNode } from "react";
import { AuthContext } from "../context/AuthContext";
import { authService } from "../services/authService";
import type { AuthResponse } from "../types/contracts/responses/AuthResponse";
import type { UserRequest } from "../types/contracts/requests/UserRequest";

interface AuthProviderProps {
    children: ReactNode;
}

interface ApiError {
    response?: {
        data?: {
            errors?: Record<string, string[]>;
            title?: string;
        };
    };
}

export function AuthProvider({ children }: AuthProviderProps) {
    const [user, setUser] = useState<string | null>(localStorage.getItem("user"));
    const [role, setRole] = useState<number | null>(
        localStorage.getItem("userRole") ? Number(localStorage.getItem("userRole")) : null
    );

    const [error, setError] = useState<string | null>(null);

    const handleError = (err: unknown) => {
        if (typeof err === "object" && err !== null && "response" in err) {
            const apiError = err as ApiError;
            if (apiError.response?.data?.errors) {
                const messages = Object.values(apiError.response.data.errors).flat().join(", ");
                setError(messages);
            } else if (apiError.response?.data?.title) {
                setError(apiError.response.data.title);
            } else {
                setError("Unknown server error");
            }
        } else if (err instanceof Error) {
            setError(err.message);
        } else if (typeof err === "string") {
            setError(err);
        } else {
            setError("Unknown error occurred");
        }
    };

    const login = async (email: string, password: string) => {
        try {
            const data: AuthResponse = await authService.logIn({ email, password });
            localStorage.setItem("accessToken", data.accessToken);
            localStorage.setItem("refreshToken", data.refreshToken);
            localStorage.setItem("user", data.email);
            localStorage.setItem("userRole", String(data.userRole));
            setUser(data.email);
            setRole(data.userRole);
            setError(null);
        } catch (err: unknown) {
            handleError(err);
            throw err;
        }
    };

    const register = async (req: UserRequest) => {
        try {
            const data: AuthResponse = await authService.signUp(req);
            localStorage.setItem("accessToken", data.accessToken);
            localStorage.setItem("refreshToken", data.refreshToken);
            localStorage.setItem("user", data.email);
            localStorage.setItem("userRole", String(req.userRole));
            setUser(data.email);
            setRole(req.userRole);
            setError(null);
        } catch (err: unknown) {
            handleError(err);
            throw err;
        }
    };

    const logout = async () => {
        try {
            await authService.logOut();
        } catch (err: unknown) {
            console.error("Logout failed", err);
        } finally {
            localStorage.clear();
            setUser(null);
            setRole(null);
            setError(null);
        }
    };

    return (
        <AuthContext.Provider value={{ user, role, error, login, register, logout }}>
            {children}
        </AuthContext.Provider>
    );
}
