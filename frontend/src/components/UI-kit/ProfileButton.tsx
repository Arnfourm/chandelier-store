import { Link } from "react-router-dom";

interface ProfileButtonProps {
    name: string;
    ref: string;
}

export function ProfileButton({ name, ref }: ProfileButtonProps) {
    return (
        <Link
            to={ref}
            className="w-[200px] h-20 p-10 flex justify-around items-center border-l-2 border-l-neutral-200 text-xl"
        >
            <img src="icons/profile-icon.png" alt="Profile icon" className="w-[26px] h-[26px]" />
            <span>{name}</span>
        </Link>
    );
}
