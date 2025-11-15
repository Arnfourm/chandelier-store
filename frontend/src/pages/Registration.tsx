import { AuthSwitchSection } from "../modules/AuthSwitchSection";
import { RegistrationForm } from "../modules/RegistrationForm";

export function Registration() {
    return (
        <div className="w-full h-full flex">
            <RegistrationForm />

            <AuthSwitchSection
                headingName={"Уже есть аккаунт?"}
                text={
                    "Войдите в аккаунт, чтобы получить полный доступ к функционалу интернет-магазина!"
                }
                buttonName={"Войти"}
                buttonRef={"/login"}
                headerClassName={"ml-[100px] text-neutral-500"}
            />
        </div>
    );
}
