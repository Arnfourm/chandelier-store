// contexts/AuthContext.tsx
import { createContext, useContext, useState, useEffect } from "react";

interface AuthContextType {
    accessToken: string | null;
    refreshToken: string | null;
    role: number | null;
    email: string | null;
    user: any | null;
    isAuthenticated: boolean;
    isInitializing: boolean;
    login: (email: string, password: string) => Promise<number | null>;
    logout: () => void;
    refresh: () => Promise<boolean>;
}

const AuthContext = createContext<AuthContextType | null>(null);

export function useAuth() {
    const context = useContext(AuthContext);
    if (!context) {
        throw new Error("useAuth must be used within AuthProvider");
    }
    return context;
}

export function AuthProvider({ children }: { children: React.ReactNode }) {
    const [accessToken, setAccessToken] = useState<string | null>(null);
    const [refreshToken, setRefreshToken] = useState<string | null>(null);
    const [role, setRole] = useState<number | null>(null);
    const [email, setEmail] = useState<string | null>(null);
    const [user, setUser] = useState<any | null>(null);
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [isInitializing, setIsInitializing] = useState(true);

    const decodeJWT = (token: string) => {
        try {
            const base64Url = token.split(".")[1];
            const base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
            const jsonPayload = decodeURIComponent(
                atob(base64)
                    .split("")
                    .map((c) => "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2))
                    .join("")
            );
            return JSON.parse(jsonPayload);
        } catch (error) {
            console.error("Ошибка декодирования JWT:", error);
            return null;
        }
    };

    const saveAuthToStorage = (data: {
        accessToken: string;
        refreshToken: string;
        userRole: number;
        email: string;
        userId?: string;
        name?: string;
        surname?: string;
    }) => {
        localStorage.setItem("accessToken", data.accessToken);
        localStorage.setItem("refreshToken", data.refreshToken);

        localStorage.setItem(
            "auth_tokens",
            JSON.stringify({
                accessToken: data.accessToken,
                refreshToken: data.refreshToken,
            })
        );
        localStorage.setItem("user_email", data.email);
        localStorage.setItem("user_role", data.userRole.toString());
        if (data.userId) {
            localStorage.setItem("user_id", data.userId);
        }
        if (data.name) {
            localStorage.setItem("user_name", data.name);
        }
        if (data.surname) {
            localStorage.setItem("user_surname", data.surname);
        }
    };

    const clearAuthFromStorage = () => {
        localStorage.removeItem("accessToken");
        localStorage.removeItem("refreshToken");
        localStorage.removeItem("auth_tokens");
        localStorage.removeItem("user_email");
        localStorage.removeItem("user_role");
        localStorage.removeItem("user_id");
        localStorage.removeItem("user_name");
        localStorage.removeItem("user_surname");
    };

    const getAuthFromStorage = () => {
        let tokens = null;
        const tokensStr = localStorage.getItem("auth_tokens");

        if (tokensStr) {
            tokens = JSON.parse(tokensStr);
        } else {
            const accessToken = localStorage.getItem("accessToken");
            const refreshToken = localStorage.getItem("refreshToken");
            if (accessToken && refreshToken) {
                tokens = { accessToken, refreshToken };
            }
        }

        const email = localStorage.getItem("user_email") || localStorage.getItem("email");
        const roleStr = localStorage.getItem("user_role") || localStorage.getItem("userRole");

        if (!tokens || !email || !roleStr) return null;

        return {
            tokens,
            email,
            role: parseInt(roleStr, 10),
        };
    };

    useEffect(() => {
        const initializeAuth = () => {
            const storedAuth = getAuthFromStorage();

            if (storedAuth && storedAuth.tokens.accessToken) {
                try {
                    const decoded = decodeJWT(storedAuth.tokens.accessToken);

                    if (decoded) {
                        const isExpired = decoded.exp ? decoded.exp * 1000 < Date.now() : false;

                        if (!isExpired) {
                            setAccessToken(storedAuth.tokens.accessToken);
                            setRefreshToken(storedAuth.tokens.refreshToken);
                            setEmail(storedAuth.email);
                            setRole(storedAuth.role);
                            setIsAuthenticated(true);

                            const userName =
                                localStorage.getItem("user_name") ||
                                decoded.given_name ||
                                decoded.name ||
                                "";
                            const userSurname =
                                localStorage.getItem("user_surname") ||
                                decoded.family_name ||
                                decoded.surname ||
                                "";
                            const userId =
                                localStorage.getItem("user_id") ||
                                decoded.userId ||
                                decoded.nameid ||
                                "";

                            setUser({
                                userId: userId,
                                email: storedAuth.email,
                                userRole: storedAuth.role,
                                name: userName,
                                surname: userSurname,
                            });

                            console.log("Авторизация восстановлена из localStorage");
                        } else {
                            console.log("Токен истек");
                            clearAuthFromStorage();
                        }
                    }
                } catch (error) {
                    console.error("Ошибка при восстановлении авторизации:", error);
                    clearAuthFromStorage();
                }
            }

            setIsInitializing(false);
        };

        initializeAuth();
    }, []);

    const login = async (email: string, password: string): Promise<number | null> => {
        try {
            console.log("Отправляем запрос на логин:", { email });

            const res = await fetch("http://localhost:9230/api/Users/Auth/LogIn", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Accept: "application/json",
                },
                body: JSON.stringify({ email, password }),
            });

            console.log("Статус ответа:", res.status);

            if (res.status === 401) {
                console.log("Неверные учетные данные");
                return null;
            }

            if (!res.ok) {
                console.error("Ошибка сервера:", res.status);
                return null;
            }

            const data = await res.json();
            console.log("Данные ответа:", data);

            const decoded = decodeJWT(data.accessToken);
            console.log("Декодированный токен:", decoded);

            let userName = "";
            let userSurname = "";

            if (decoded) {
                userName =
                    decoded.given_name ||
                    decoded.name ||
                    decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"] ||
                    decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"] ||
                    "";

                userSurname =
                    decoded.family_name ||
                    decoded.surname ||
                    decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname"] ||
                    "";
            }

            if (!userName && data.name) userName = data.name;
            if (!userSurname && data.surname) userSurname = data.surname;

            console.log("Извлеченные данные:", { userName, userSurname });

            const userData = {
                userId: data.userId || decoded?.userId || decoded?.nameid || "",
                email: data.email || email,
                userRole: data.userRole,
                name: userName,
                surname: userSurname,
                profile: data,
            };

            console.log("Создан объект пользователя:", userData);

            setAccessToken(data.accessToken);
            setRefreshToken(data.refreshToken);
            setEmail(data.email || email);
            setRole(data.userRole);
            setUser(userData);
            setIsAuthenticated(true);

            saveAuthToStorage({
                accessToken: data.accessToken,
                refreshToken: data.refreshToken,
                userRole: data.userRole,
                email: data.email || email,
                userId: data.userId,
                name: userName,
                surname: userSurname,
            });

            console.log("Логин успешен. Роль пользователя:", data.userRole);
            return data.userRole;
        } catch (err) {
            console.error("Ошибка при логине:", err);
            return null;
        }
    };

    const logout = () => {
        console.log("Выполняем логаут");
        setAccessToken(null);
        setRefreshToken(null);
        setEmail(null);
        setRole(null);
        setUser(null);
        setIsAuthenticated(false);
        clearAuthFromStorage();
    };

    const refresh = async (): Promise<boolean> => {
        if (!refreshToken) {
            console.log("Нет refresh токена");
            return false;
        }

        try {
            const res = await fetch("http://localhost:9230/api/Users/Auth/RefreshToken", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Accept: "application/json",
                },
                body: JSON.stringify({ refreshToken }),
            });

            if (!res.ok) {
                console.error("Ошибка обновления токена:", res.status);
                logout();
                return false;
            }

            const data = await res.json();

            setAccessToken(data.accessToken);
            setRefreshToken(data.refreshToken);
            setRole(data.userRole);

            saveAuthToStorage({
                accessToken: data.accessToken,
                refreshToken: data.refreshToken,
                userRole: data.userRole,
                email: email || "",
            });

            console.log("Токен успешно обновлен");
            return true;
        } catch (err) {
            console.error("Ошибка при обновлении токена:", err);
            logout();
            return false;
        }
    };

    const value: AuthContextType = {
        accessToken,
        refreshToken,
        role,
        email,
        user,
        isAuthenticated,
        isInitializing,
        login,
        logout,
        refresh,
    };

    return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}
