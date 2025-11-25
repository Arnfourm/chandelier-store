import api from "../http/axiosInstance";
import type { LoginRequest } from "../types/contracts/requests/LoginRequest";
import type { UserRequest } from "../types/contracts/requests/UserRequest";
import type { AuthResponse } from "../types/contracts/responses/AuthResponse";

export const authService = {
    signUp: async (data: UserRequest): Promise<AuthResponse> => {
        const res = await api.post<AuthResponse>("/Users/Auth/SignUp", data);
        return res.data;
    },
    logIn: async (data: LoginRequest): Promise<AuthResponse> => {
        const res = await api.post<AuthResponse>("/Users/Auth/LogIn", data);
        return res.data;
    },
    logOut: async (): Promise<void> => {
        await api.post("/Users/Auth/LogOut");
        localStorage.clear();
    },
};
