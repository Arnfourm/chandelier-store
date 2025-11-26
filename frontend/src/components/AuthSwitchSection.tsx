import { Logo } from "./Logo";
import { SwitchButton } from "./SwitchButton";

interface AuthSwitchSectionProps {
    headingName: string;
    text: string;
    buttonName: string;
    buttonRef: string;
    headerClassName: string;
}

export function AuthSwitchSection({
    headingName,
    text,
    buttonName,
    buttonRef,
    headerClassName = "",
}: AuthSwitchSectionProps) {
    return (
        <div className="h-full w-[35%] bg-stone-900 pt-[35px] flex flex-col">
            <header className={headerClassName.includes("text-right") ? "text-right" : ""}>
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
