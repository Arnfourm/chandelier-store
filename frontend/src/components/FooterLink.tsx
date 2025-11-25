import { Link } from "react-router-dom";
import { type RedirectLinkProps } from "../types/RedirectLinkPropsType";

export function FooterLink({ name, ref }: RedirectLinkProps) {
    return (
        <Link to={ref} className="text-x1 text-neutral-500">
            {name}
        </Link>
    );
}
