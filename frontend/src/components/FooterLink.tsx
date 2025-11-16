import { Link } from "react-router-dom";

export function FooterLink({ name, ref }) {
    return (
        <Link to={ref} className="text-x1 text-neutral-500">
            {name}
        </Link>
    );
}
