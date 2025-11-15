export function FooterLink({ name, ref }) {
    return (
        <a className="text-x1 text-neutral-500" href={ref}>
            {name}
        </a>
    );
}
