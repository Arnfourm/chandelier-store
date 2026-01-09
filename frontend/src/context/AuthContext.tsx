/* 
Контекст авторизации 
Глобальное хранилище данных для авторизации (или полученные после нее) - токены, информация о пользователе...

AuthProvider - сам склад, из которого достаются данные при помощи хука useAuth
useEffect - хук, который инициализирует контекст при загрузке страницы
+ методы для авторизации
*/

// const auth = useAuth();

// useEffect(() => {
//     auth.logout();
//     console.log("Авторизация сброшена при инициализации");
// }, []);

import { createContext, useContext, useState, useEffect } from "react";
import { saveAuth, getAuth, clearAuth } from "../storage/AuthStorage";

const AuthContext = createContext(null);

export function useAuth() {
    return useContext(AuthContext);
}

export function AuthProvider({ children }) {
    const [accessToken, setAccessToken] = useState(null);
    const [refreshToken, setRefreshToken] = useState(null);

    const [role, setRole] = useState(null);
    const [email, setEmail] = useState(null);
    const [user, setUser] = useState(null);

    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [isInitializing, setIsInitializing] = useState(true);

    const fetchUserProfile = async (token, userEmail) => {
        if (!userEmail || !token) {
            console.log("Нет email или токена для профиля");
            return;
        }

        try {
            const resUser = await fetch(
                `http://localhost:9230/api/Users/User/${encodeURIComponent(userEmail)}`,
                {
                    method: "GET",
                    headers: {
                        accept: "text/plain",
                        Authorization: `Bearer ${token}`,
                    },
                }
            );

            if (resUser.ok) {
                const userDataString = await resUser.text();
                const userData = JSON.parse(userDataString);
                setUser({
                    email: userData.email,
                    name: userData.name,
                    surname: userData.surname,
                    role: userData.userRole,
                    profile: userData,
                });
                setRole(userData.userRole);
            } else {
                console.error(resUser.status, await resUser.text());
            }
        } catch (err) {
            console.error("Ошибка профиля:", err);
        }
    };

    useEffect(() => {
        const auth = getAuth();
        if (auth && localStorage.getItem("userEmail")) {
            setAccessToken(auth.accessToken);
            setRefreshToken(auth.refreshToken);
            setIsAuthenticated(true);

            const storedRole = localStorage.getItem("userRole");
            if (storedRole) setRole(parseInt(storedRole, 10));

            const storedEmail = localStorage.getItem("userEmail");
            fetchUserProfile(auth.accessToken, storedEmail);
        }
        setIsInitializing(false);
    }, []);

    const login = async (email, password) => {
        try {
            const res = await fetch("http://localhost:9230/api/Users/Auth/LogIn", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ email, password }),
            });

            if (res.status === 401) return null;

            const data = await res.json();
            setAccessToken(data.accessToken);
            setRefreshToken(data.refreshToken);
            setRole(data.userRole);

            await fetchUserProfile(data.accessToken, email);

            saveAuth({ accessToken: data.accessToken, refreshToken: data.refreshToken });
            localStorage.setItem("userEmail", email);
            localStorage.setItem("userRole", data.userRole);

            setIsAuthenticated(true);

            return data.userRole;
        } catch (err) {
            console.error("Ошибка логина:", err);
            return null;
        }
    };

    const logout = () => {
        setAccessToken(null);
        setRefreshToken(null);
        setEmail(null);
        setRole(null);
        setUser(null);
        setIsAuthenticated(false);
        clearAuth();
        localStorage.removeItem("userEmail");
        localStorage.removeItem("userRole");
    };

    const refresh = async () => {
        if (!refreshToken) return false;
        try {
            const res = await fetch("http://localhost:9230/api/Users/Auth/RefreshToken", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ refreshToken }),
            });

            if (!res.ok) {
                logout();
                return false;
            }

            const data = await res.json();
            setAccessToken(data.accessToken);
            setRefreshToken(data.refreshToken);
            setRole(data.userRole);
            setIsAuthenticated(true);

            await fetchUserProfile(data.accessToken, email);

            saveAuth({ accessToken: data.accessToken, refreshToken: data.refreshToken });
            localStorage.setItem("userRole", data.userRole);
            return true;
        } catch (err) {
            console.error("Ошибка refresh:", err);
            logout();
            return false;
        }
    };

    const value = {
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
