import { useShoppingCart } from "../../context/ShoppingCartContext"; // путь к твоему контексту
import { useAuth } from "../../context/AuthContext"; // твой контекст авторизации

export function CartButton() {
    const { openCart, cartQuantity } = useShoppingCart();
    const { isAuthenticated } = useAuth();

    return (
        <div className="relative p-[25px] border-l-2 border-neutral-200 group">
            <button
                onClick={openCart}
                className="flex items-center justify-center"
                disabled={!isAuthenticated}
            >
                <img
                    src="icons/cart-icon.png"
                    alt="Корзина"
                    className="w-[30px] h-[30px] transition-transform duration-300 hover:scale-110"
                />
            </button>

            {isAuthenticated && cartQuantity > 0 && (
                <div className="w-[25px] h-[25px] absolute top-3 right-4 flex items-center justify-center bg-red-400 text-white text-xs rounded-full">
                    {cartQuantity}
                </div>
            )}
        </div>
    );
}
