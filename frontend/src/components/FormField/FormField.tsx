export function FormField({ label, name, type, placeholder }) {
    return (
        <>
            <label>{label}</label>
            <input type={type} name={name} placeholder={placeholder}></input>
        </>
    );
}
