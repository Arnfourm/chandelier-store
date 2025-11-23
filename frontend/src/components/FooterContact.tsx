import { type ContactProps } from "../types/ContactPropsType";

export function FooterContact({ label, info }: ContactProps) {
    return (
        <div>
            <p className="mb-2 text-neutral-500">{label}</p>
            <p className="text-4xl text-neutral-300 font-extralight cursor-pointer">{info}</p>
        </div>
    );
}
