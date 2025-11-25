export interface FormFieldProps {
    label: string;
    name: string;
    type: string;
    placeholder: string;
    className: string;
    value?: string;
    onChange?: (e: React.ChangeEvent<HTMLInputElement>) => void;
}
