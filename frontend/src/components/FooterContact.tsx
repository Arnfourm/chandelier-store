export function FooterContact({ label, info }) {
    return (
        <div>
            <p className="text-neutral-500 mb-2">{label}</p>
            <p className="text-4xl text-neutral-300 font-extralight cursor-pointer">{info}</p>
        </div>
    );
}
