export async function apiFetch(url, options = {}, { accessToken, refreshToken, refresh, logout }) {
    let headers = options.headers ? { ...options.headers } : {};

    if (accessToken) {
        headers["Authorization"] = `Bearer ${accessToken}`;
    }

    let res = await fetch(url, { ...options, headers });

    if (res.status === 401 && refreshToken) {
        console.log("AccessToken просрочен, пробуем обновить");
        const refreshed = await refresh();
        if (refreshed) {
            headers["Authorization"] = `Bearer ${accessToken}`;
            res = await fetch(url, { ...options, headers });
        } else {
            console.warn("Обновление токена не удалось, делаем logout");
            logout();
        }
    }

    return res;
}
