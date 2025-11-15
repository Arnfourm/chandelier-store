import { Logo } from "../components/Logo";
import { SwitchButton } from "../components/SwitchButton";

export function AuthSwitchSection({
    headingName,
    text,
    buttonName,
    buttonRef,
    headerClassName = "",
}) {
    return (
        <div className="h-full w-[35%] bg-stone-900 pt-[35px] flex flex-col">
            <header>
                <Logo className={headerClassName} />
            </header>

            <div className="flex-1 flex flex-col items-center text-center mt-[315px]">
                <h2 className="text-neutral-200 font-semibold text-4xl">{headingName}</h2>
                <p className="w-[500px] text-neutral-500 text-xl mt-[35px] mb-[60px]">{text}</p>
                <SwitchButton name={buttonName} ref={buttonRef}></SwitchButton>
            </div>
        </div>
    );
}
