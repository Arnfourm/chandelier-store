export function Contact({ label, info }) {
    return (
        <div className="w-[250px] flex flex-col items-center justify-center text-center mt-[40px] mb-[40px]">
            <h2 className="text-neutral-500 text-xl mb-1">{label}</h2>
            <p className="text-xl">{info}</p>
        </div>
    );
}
