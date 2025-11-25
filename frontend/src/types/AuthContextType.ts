import type { UserRequest } from "../types/contracts/requests/UserRequest";

export interface AuthContextType {
    user: string | null;
    role: number | null;
    error: string | null;
    login: (email: string, password: string) => Promise<void>;
    register: (data: UserRequest) => Promise<void>;
    logout: () => Promise<void>;
}
