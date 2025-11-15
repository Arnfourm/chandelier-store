import { Link } from "react-router-dom";

export function HomeButton({ name, ref }) {
    return (
        <div className="flex cursor-pointer">
            <Link to={ref} className="font-semibold text-xl mr-[10px]">
                {name}
            </Link>
            <div className="w-[28px] h-[28px] bg-amber-600 flex items-center justify-center rounded-full">
                <span className="text-2xl text-neutral-100 font-bold">&#8250;</span>
            </div>
        </div>
    );
}
