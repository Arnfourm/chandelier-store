import { type ContactProps } from "../types/ContactPropsType";

export function Contact({ label, info }: ContactProps) {
    return (
        <div className="w-[250px] mt-10 mb-10 flex flex-col items-center justify-center text-center">
            <h2 className="mb-1 text-xl text-neutral-500">{label}</h2>
            <p className="text-xl">{info}</p>
        </div>
    );
}
