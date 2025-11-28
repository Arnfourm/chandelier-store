import { type FormFieldPropsType } from "../../types/FormFieldPropsType";

export function FormField({
    label,
    name,
    type,
    placeholder,
    value,
    onChange,
    className = "",
}: FormFieldPropsType) {
    return (
        <div className="flex flex-col items-start gap-y-1.5">
            <label className="text-2xl text-stone-900 font-medium">{label}</label>
            <input
                name={name}
                type={type}
                placeholder={placeholder}
                value={value}
                onChange={onChange}
                className={`h-20 p-5 text-2xl text-neutral-500 border-2 border-solid border-stone-900 ${className}`}
            />
        </div>
    );
}
