import { PromoSection } from "../modules/PromoSection/PromoSection";
import { LoginForm } from "../modules/LoginForm/LoginForm";

export function LogIn() {
    return (
        <div className="w-full h-full flex">
            <PromoSection />
            <LoginForm />
        </div>
    );
}
