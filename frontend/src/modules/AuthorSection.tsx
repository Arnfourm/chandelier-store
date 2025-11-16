import { Link } from "react-router-dom";

export function AuthorSection() {
    return (
        <div className="h-[80px] text-x1 text-neutral-500 flex justify-between items-center pl-[100px] pr-[100px]">
            <Link to="https://github.com/Arnfourm/chandelier-store">
                Проект разработан A&V в учебных целях
            </Link>
            <p>Технологии программирования</p>
        </div>
    );
}
