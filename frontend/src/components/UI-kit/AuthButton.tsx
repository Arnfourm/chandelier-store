export function AuthButton() {
    return (
        <button type="submit" disabled={loading}>
            {loading ? "Входим..." : "Войти"}
        </button>
    );
}
