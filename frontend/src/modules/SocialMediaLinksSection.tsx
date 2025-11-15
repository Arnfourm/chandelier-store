import { Logo } from "../components/Logo";

export function SocialMediaLinksSection() {
    return (
        <div className="w-[160px] flex flex-col gap-5">
            <Logo className="text-neutral-200 text-4xl text-wrap" />

            <div className="flex items-center justify-between opacity-[0.5]">
                <a href="">
                    <img src="icons/vk-icon.png" alt="VK icon" className="h-[42px] w-[42px]" />
                </a>
                <a href="">
                    <img
                        src="icons/telegram-icon.png"
                        alt="Telegram icon"
                        className="h-[35px] w-[35px]"
                    />
                </a>
                <a href="">
                    <img
                        src="icons/whatsapp-icon.png"
                        alt="WhatsApp icon"
                        className="h-[35px] w-[35px]"
                    />
                </a>
            </div>
        </div>
    );
}
