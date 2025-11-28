interface AuthInputProps {
    name: string;
    type: string;
    placeholder: string;
    value: string;
    onChange: React.InputHTMLAttributes<HTMLInputElement>;
    className: string;
}

export function AuthInput({
    name,
    type,
    placeholder,
    value,
    onChange,
    className = "",
}: AuthInputProps) {
    return (
        <input
            name={name}
            type={type}
            placeholder={placeholder}
            value={value}
            onChange={onChange}
            className={className}
        />
    );
}
