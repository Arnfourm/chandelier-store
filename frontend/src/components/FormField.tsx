export function FormField({ label, name, type, placeholder, className = "" }) {
    return (
        <div className="flex flex-col items-start gap-y-1.5">
            <label className="text-stone-900 text-2xl font-medium">{label}</label>
            <input
                className={`h-[80px] border-2 border-solid border-stone-900 text-neutral-500 text-2xl p-5 ${className}`}
                type={type}
                name={name}
                placeholder={placeholder}
            />
        </div>
    );
}
