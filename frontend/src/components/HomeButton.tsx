import { Link } from "react-router-dom";
import type { RedirectLinkProps } from "../types/RedirectLinkPropsType";

export function HomeButton({ name, ref }: RedirectLinkProps) {
    return (
        <div className="flex cursor-pointer">
            <Link to={ref} className="mr-2.5 text-xl font-semibold">
                {name}
            </Link>

            <div className="w-7 h-7 flex items-center justify-center rounded-full bg-amber-500">
                <span className="text-2xl text-neutral-50 font-bold">&#8250;</span>
            </div>
        </div>
    );
}
