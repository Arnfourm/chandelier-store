import { AboutSection } from "./AboutSection";
import { AuthorSection } from "./AuthorSection";
import { ContactsSection } from "./ContactsSection";
import { ForClientsSection } from "./ForClientsSection";
import { SocialMediaLinksSection } from "./SocialMediaLinksSection";

export function Footer() {
    return (
        <footer className="w-full h-[400px] bg-stone-900">
            <div className="h-[320px] border-b-2 border-b-neutral-700 pl-[150px] pr-[150px] pt-[50px] flex justify-between">
                <SocialMediaLinksSection />
                <ForClientsSection />
                <AboutSection />
                <ContactsSection />
            </div>

            <AuthorSection />
        </footer>
    );
}
