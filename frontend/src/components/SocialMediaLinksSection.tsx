import { Logo } from "./Logo";

export function SocialMediaLinksSection() {
    return (
        <div className="w-40 flex flex-col gap-5">
            <Logo className="text-4xl text-wrap text-neutral-200" />

            <div className="flex items-center justify-between opacity-[0.5]">
                <a href="https://vk.com/">
                    <img src="icons/vk-icon.png" alt="VK icon" className="h-[42px] w-[42px]" />
                </a>

                <a href="https://web.telegram.org/">
                    <img
                        src="icons/telegram-icon.png"
                        alt="Telegram icon"
                        className="h-[35px] w-[35px]"
                    />
                </a>

                <a href="https://www.whatsapp.com/?lang=ru_RU">
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
