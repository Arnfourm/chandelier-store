const decodeJWT = (token) => {
    try {
        const base64Url = token.split(".")[1];
        const base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
        const jsonPayload = atob(base64);
        return JSON.parse(jsonPayload);
    } catch (error) {
        return null;
    }
};
