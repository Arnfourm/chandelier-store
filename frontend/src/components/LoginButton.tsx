import { Link } from "react-router-dom";
import type { RedirectLinkProps } from "../types/RedirectLinkPropsType";

export function LoginButton({ name, ref }: RedirectLinkProps) {
    return (
        <div className="w-[200px] h-20 p-10 flex justify-around items-center text-xl border-l-2 border-l-neutral-200 cursor-pointer">
            <Link to={ref}>{name}</Link>
            <img src="icons/profile-icon.png" alt="Profile icon" className="w-[30px] h-[30px]" />
        </div>
    );
}
