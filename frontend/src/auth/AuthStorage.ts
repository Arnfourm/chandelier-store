// Хранилище токенов
export function saveAuth({ accessToken, refreshToken }) {
    console.log("Сохраняем токены в localStorage");
    localStorage.setItem("accessToken", accessToken);
    localStorage.setItem("refreshToken", refreshToken);
}

export function getAuth() {
    const accessToken = localStorage.getItem("accessToken");
    const refreshToken = localStorage.getItem("refreshToken");
    if (!accessToken || !refreshToken) return null;
    return { accessToken, refreshToken };
}

export function clearAuth() {
    console.log("Очищаем токены из localStorage");
    localStorage.removeItem("accessToken");
    localStorage.removeItem("refreshToken");
}
