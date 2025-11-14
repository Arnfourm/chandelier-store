import { AuthSwitchSection } from "../modules/AuthSwitchSection";
import { LoginForm } from "../modules/LoginForm";

export function LogIn() {
    return (
        <div className="w-full h-full flex">
            <AuthSwitchSection
                headingName={"Ещё нет аккаунта?"}
                text={"Создайте аккаунт, чтобы получить полный доступ к вашим заказам и отзывам"}
                buttonName={"Зарегистрироваться"}
            />
            <LoginForm />
        </div>
    );
}
