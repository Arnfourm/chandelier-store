import { type FormButtonPropsType } from "../../types/FormButtonPropsType";

export function FormButton({ type, name, disabled }: FormButtonPropsType) {
    return (
        <button
            type={type}
            disabled={disabled}
            className="w-[800px] h-20 bg-stone-900 text-2xl font-semibold text-neutral-100 cursor-pointer"
        >
            {name}
        </button>
    );
}
