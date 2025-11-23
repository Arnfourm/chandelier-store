import { type FormFieldProps } from "../types/FormFieldPropsType";

export function FormField({ label, name, type, placeholder, className = "" }: FormFieldProps) {
    return (
        <div className="flex flex-col items-start gap-y-1.5">
            <label className="text-2xl text-stone-900 font-medium">{label}</label>
            <input
                name={name}
                type={type}
                placeholder={placeholder}
                className={`h-20 p-5 text-2xl text-neutral-500 border-2 border-solid border-stone-900 ${className}`}
            />
        </div>
    );
}
