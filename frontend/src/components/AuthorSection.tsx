import { Link } from "react-router-dom";

export function AuthorSection() {
    return (
        <div className="h-20 pl-[100px] pr-[100px] flex justify-between items-center text-x1 text-neutral-500">
            <Link to="https://github.com/Arnfourm/chandelier-store">
                Проект разработан A&V в учебных целях
            </Link>
            <p>Технологии программирования</p>
        </div>
    );
}
