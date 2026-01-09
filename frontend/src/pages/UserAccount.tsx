import { Link } from "react-router-dom";
import { useAuth } from "../context/AuthContext";
import { useNavigate } from "react-router-dom";

export function UserAccount() {
    const { user, logout } = useAuth();
    const navigate = useNavigate();

    const handleLogout = () => {
        logout();
        navigate("/");
    };
    return (
        <>
            <div className="bg-white overflow-hidden w-full min-w-[1920px] min-h-[1024px] relative">
                <div className="absolute top-[87px] -left-px w-[446px] h-[938px] bg-white border border-solid border-[#00000026]" />

                <div className="absolute -top-px -left-px w-[1922px] h-[89px] bg-[#201f1f] border border-solid border-white" />

                <Link
                    to="/"
                    className="absolute top-[calc(50.00%_-_482px)] left-[50px] w-[246px] [font-family:'Arial-Bold',Helvetica] font-bold text-[#ffffff80] text-3xl tracking-[0] leading-[30px] whitespace-nowrap"
                >
                    Империя люстр
                </Link>

                <div className="absolute top-[calc(50.00%_-_375px)] left-[546px] w-[1077px] [font-family:'Arial-Bold',Helvetica] font-bold text-[#202020bf] text-5xl tracking-[0] leading-[48px] whitespace-nowrap">
                    Здравствуйте, {user?.name} {user?.surname}!
                </div>

                <div className="absolute top-[calc(50.00%_-_269px)] left-[546px] w-[1077px] [font-family:'Arial-Regular',Helvetica] font-normal text-[#202020bf] text-[32px] tracking-[0] leading-8">
                    Это личный кабинет пользователя
                </div>

                <div className="top-[123px] -left-px w-[449px] h-[65px] flex-col gap-9 absolute flex">
                    <div className="ml-[51px] w-[284px] h-7 [font-family:'Arial-Regular',Helvetica] font-normal text-x-5pp-lw-c text-[28px] tracking-[0] leading-7 whitespace-nowrap">
                        Управление заказами
                    </div>

                    <div className="w-[447px] h-px bg-ud-bq-t3" />
                </div>

                <div className="top-56 -left-0.5 w-[449px] h-[65px] flex-col gap-9 absolute flex">
                    <div className="ml-[51px] w-[290px] h-7 [font-family:'Arial-Regular',Helvetica] font-normal text-x-5pp-lw-c text-[28px] tracking-[0] leading-7 whitespace-nowrap">
                        Управление отзывами
                    </div>

                    <div className="w-[447px] h-px bg-ud-bq-t3" />
                </div>

                <div className="top-[325px] left-[49px] w-36 h-7 absolute flex">
                    <div className="w-[142px] h-7 [font-family:'Arial-Regular',Helvetica] font-normal text-x-5pp-lw-c text-[28px] tracking-[0] leading-7 whitespace-nowrap">
                        Избранное
                    </div>
                </div>

                <div className="top-[959px] left-[180px] w-[85px] h-7 absolute flex">
                    <button
                        onClick={handleLogout}
                        className="w-[83px] h-7 [font-family:'Arial-Regular',Helvetica] font-normal text-[#ff8d28] text-[28px] tracking-[0] leading-7 whitespace-nowrap"
                    >
                        Выйти
                    </button>
                </div>
            </div>
        </>
    );
}
