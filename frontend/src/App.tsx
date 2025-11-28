import { BrowserRouter, Routes, Route } from "react-router-dom";
import { Home } from "./pages/Home";
import { Catalog } from "./pages/Catalog";
import { Contacts } from "./pages/Contacts";
import { LogIn } from "./pages/LogIn";
import { Registration } from "./pages/Registration";
import { UserAccount } from "./pages/UserAccount";
import { AdminPanel } from "./pages/AdminPanel";

import { PrivateRoute } from "./components/PrivateRoute";
import { AuthProvider } from "./context/AuthContext";

function AppRoutes() {
    return (
        <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/catalog" element={<Catalog />} />
            <Route path="/contacts" element={<Contacts />} />

            <Route path="/login" element={<LogIn />} />
            <Route path="/reg" element={<Registration />} />

            <Route
                path="/account"
                element={
                    <PrivateRoute allowedRoles={[1]}>
                        <UserAccount />
                    </PrivateRoute>
                }
            />

            <Route
                path="/employee"
                element={
                    <PrivateRoute allowedRoles={[2, 3]}>
                        <AdminPanel />
                    </PrivateRoute>
                }
            />
        </Routes>
    );
}

export default function App() {
    return (
        <BrowserRouter>
            <AuthProvider>
                <AppRoutes />
            </AuthProvider>
        </BrowserRouter>
    );
}
