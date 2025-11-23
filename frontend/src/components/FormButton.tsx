import { type ButtonProps } from "../types/ButtonPropsType";

export function FormButton({ name }: ButtonProps) {
    return (
        <button className="w-[800px] h-20 bg-stone-900 text-2xl font-semibold text-neutral-100 cursor-pointer">
            {name}
        </button>
    );
}
