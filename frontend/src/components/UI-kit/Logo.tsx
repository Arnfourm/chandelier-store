import { Link } from "react-router-dom";

export function Logo({ className = "" }) {
    return (
        <Link to="/" className={`text-3xl font-bold hover:opacity-60 ${className}`}>
            Империя люстр
        </Link>
    );
}
