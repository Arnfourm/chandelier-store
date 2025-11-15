import { Link } from "react-router-dom";

export function LoginButton() {
    return (
        <div className="flex w-[200px] h-[80px] justify-around items-center p-10 text-xl border-l-2 border-l-neutral-200 cursor-pointer">
            <Link to="/login">Войти</Link>
            <img src="icons/profile-icon.png" alt="Profile icon" className="w-[30px] h-[30px]" />
        </div>
    );
}
