import "./Button.css";

export function Button({ name }) {
    return (
        <button className="w-[500px] h-[80px] border-2 border-solid border-neutral-100 text-neutral-100 text-2xl">
            {name}
        </button>
    );
}
