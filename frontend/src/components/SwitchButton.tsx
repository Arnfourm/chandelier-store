import { Link } from "react-router-dom";

export function SwitchButton({ name, ref }) {
    return (
        <Link
            to={ref}
            className="w-[500px] h-[80px] border-2 border-solid border-neutral-100 text-neutral-100 text-2xl cursor-pointer font-semibold flex items-center justify-center"
        >
            {name}
        </Link>
    );
}
