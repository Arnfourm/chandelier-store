export async function apiFetchWithJWT(
    url,
    options = {},
    { accessToken, refreshToken, refresh, logout }
) {
    const headers = { ...options.headers };
    if (accessToken) headers["Authorization"] = `Bearer ${accessToken}`;

    let res = await fetch(url, { ...options, headers });

    if (res.status === 401 && refreshToken) {
        const refreshed = await refresh();

        if (refreshed) {
            headers["Authorization"] = `Bearer ${accessToken}`;
            res = await fetch(url, { ...options, headers });
        } else {
            logout();
        }
    }
    return res;
}
