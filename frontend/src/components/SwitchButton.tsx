import { Link } from "react-router-dom";
import { type RedirectLinkProps } from "../types/RedirectLinkPropsType";

export function SwitchButton({ name, ref }: RedirectLinkProps) {
    return (
        <Link
            to={ref}
            className="w-[500px] h-20 flex justify-center items-center border-2 border-solid border-neutral-100 text-2xl text-neutral-100 font-semibold cursor-pointer"
        >
            {name}
        </Link>
    );
}
