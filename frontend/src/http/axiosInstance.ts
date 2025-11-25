import axios from "axios";

const api = axios.create({
    baseURL: "https://localhost:7217/api",
    headers: {
        "Content-Type": "application/json",
    },
});

api.interceptors.response.use(
    (response) => response,
    async (error) => {
        const originalRequest = error.config;
        if (error.response?.status === 401 && !originalRequest._retry) {
            originalRequest._retry = true;

            const refreshToken = localStorage.getItem("refreshToken");
            if (!refreshToken) return Promise.reject(error);

            try {
                const response = await axios.post(
                    "https://localhost:7217/api/Users/Auth/RefreshToken",
                    { refreshToken },
                    { headers: { "Content-Type": "application/json" } }
                );

                localStorage.setItem("accessToken", response.data.accessToken);
                localStorage.setItem("refreshToken", response.data.refreshToken);

                api.defaults.headers["Authorization"] = `Bearer ${response.data.accessToken}`;
                originalRequest.headers["Authorization"] = `Bearer ${response.data.accessToken}`;

                return api(originalRequest);
            } catch (refreshError) {
                return Promise.reject(refreshError);
            }
        }

        return Promise.reject(error);
    }
);

export default api;
