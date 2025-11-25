export interface AuthResponse {
    userId: string;
    email: string;
    userRole: number;
    accessToken: string;
    refreshToken: string;
}
