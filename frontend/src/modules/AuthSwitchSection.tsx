import { Logo } from "../components/Logo";
import { SwitchButton } from "../components/SwitchButton";

export function AuthSwitchSection({ headingName, text, buttonName }) {
    return (
        <div className="h-full w-[35%] bg-stone-900 pt-[35px] flex flex-col">
            <header className="ml-[100px] text-neutral-400">
                <Logo />
            </header>

            <div className="flex-1 flex flex-col items-center text-center mt-[315px]">
                <h2 className="text-neutral-200 font-semibold text-4xl">{headingName}</h2>
                <p className="w-[500px] text-neutral-500 text-xl mt-[35px] mb-[60px]">{text}</p>
                <SwitchButton name={buttonName}></SwitchButton>
            </div>
        </div>
    );
}
