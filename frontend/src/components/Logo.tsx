import { Link } from "react-router-dom";

export function Logo({ className = "" }) {
    return (
        <Link to="/" className={`font-bold text-3xl ${className}`}>
            Империя люстр
        </Link>
    );
}
