import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import { Home } from "./pages/Home";
import { Catalog } from "./pages/Catalog";
import { Contacts } from "./pages/Contacts";
import { LogIn } from "./pages/LogIn";
import { Registration } from "./pages/Registration";
import { UserAccount } from "./pages/UserAccount";
import { AdminPanel } from "./pages/AdminPanel";
import { useAuth } from "./hooks/useAuth";

function AppRoutes() {
    const { user, role } = useAuth();

    return (
        <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/catalog" element={<Catalog />} />
            <Route path="/contacts" element={<Contacts />} />
            <Route path="/login" element={!user ? <LogIn /> : <Navigate to="/" />} />
            <Route path="/reg" element={!user ? <Registration /> : <Navigate to="/" />} />
            <Route
                path="/account"
                element={user && role === 1 ? <UserAccount /> : <Navigate to="/login" />}
            />
            <Route
                path="/employee"
                element={
                    user && (role === 2 || role === 3) ? <AdminPanel /> : <Navigate to="/login" />
                }
            />
        </Routes>
    );
}

export default function App() {
    return (
        <BrowserRouter>
            <AppRoutes />
        </BrowserRouter>
    );
}
