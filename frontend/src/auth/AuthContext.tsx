// src/auth/AuthContext.jsx
import { createContext, useContext, useState, useEffect } from "react";
import { saveAuth, getAuth, clearAuth } from "./authStorage";

const AuthContext = createContext(null);

export function useAuth() {
    return useContext(AuthContext);
}

export function AuthProvider({ children }) {
    const [accessToken, setAccessToken] = useState(null);
    const [refreshToken, setRefreshToken] = useState(null);
    const [roles, setRoles] = useState([]);
    const [isAuthenticated, setIsAuthenticated] = useState(false);

    const [isInitializing, setIsInitializing] = useState(true);

    useEffect(() => {
        const auth = getAuth();
        if (auth) {
            console.log("Инициализация контекста из localStorage:", auth);
            setAccessToken(auth.accessToken);
            setRefreshToken(auth.refreshToken);

            const storedRole = localStorage.getItem("userRole");
            if (storedRole) setRoles([parseInt(storedRole, 10)]);

            setIsAuthenticated(true);
        }
        setIsInitializing(false);
    }, []);

    const login = async (email, password) => {
        console.log("Попытка логина с", email, password);
        try {
            const res = await fetch("http://localhost:9230/api/Users/Auth/LogIn", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ email, password }),
            });

            if (res.status === 401) {
                console.warn("Неверные данные для логина");
                return null;
            }

            const data = await res.json();
            console.log("Ответ от сервера логина:", data);

            setAccessToken(data.accessToken);
            setRefreshToken(data.refreshToken);
            setRoles([data.userRole]);
            setIsAuthenticated(true);

            saveAuth({ accessToken: data.accessToken, refreshToken: data.refreshToken });
            localStorage.setItem("userRole", data.userRole);

            return data.userRole;
        } catch (err) {
            console.error("Ошибка логина:", err);
            return null;
        }
    };

    const logout = () => {
        console.log("Выход пользователя");
        setAccessToken(null);
        setRefreshToken(null);
        setRoles([]);
        setIsAuthenticated(false);
        clearAuth();
        localStorage.removeItem("userRole");
    };

    const refresh = async () => {
        console.log("Попытка обновления токена");
        if (!refreshToken) return false;

        try {
            const res = await fetch("http://localhost:9230/api/Users/Auth/RefreshToken", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ refreshToken }),
            });

            if (!res.ok) {
                console.warn("Не удалось обновить токен, делаем logout");
                logout();
                return false;
            }

            const data = await res.json();
            console.log("Обновленные токены:", data);

            setAccessToken(data.accessToken);
            setRefreshToken(data.refreshToken);
            setRoles([data.userRole]);
            setIsAuthenticated(true);

            saveAuth({ accessToken: data.accessToken, refreshToken: data.refreshToken });
            localStorage.setItem("userRole", data.userRole);

            return true;
        } catch (err) {
            console.error("Ошибка обновления токена:", err);
            logout();
            return false;
        }
    };

    const value = {
        accessToken,
        refreshToken,
        roles,
        isAuthenticated,
        isInitializing,
        login,
        logout,
        refresh,
    };

    return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}
