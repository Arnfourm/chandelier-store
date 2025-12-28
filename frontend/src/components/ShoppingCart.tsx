import { Link } from "react-router-dom";
import { useShoppingCart } from "../context/ShoppingCartContext";
import { CartItem } from "./CartItem";
import { formatCurrency } from "../utilities/formatCurrency";

import storeItems from "../data/items.json"; // временно

type ShoppingCartProps = {
    isOpen: boolean;
};

export function ShoppingCart({ isOpen }: ShoppingCartProps) {
    const { closeCart, cartItems } = useShoppingCart();

    if (!isOpen) return null;

    const totalAmount = cartItems.reduce((total, cartItem) => {
        const item = storeItems.find((i) => i.id === cartItem.id);
        return total + (item?.price || 0) * cartItem.quantity;
    }, 0);

    return (
        <div className="fixed inset-0 z-2 bg-gray-900/20 backdrop-blur-[2px]">
            <div className="h-full w-[400px] fixed right-0 top-0 flex flex-col bg-white">
                <div className="flex justify-between items-center p-5 border-b border-gray-300">
                    <h2 className="text-2xl">Корзина</h2>
                    <button
                        onClick={closeCart}
                        className="w-8 h-8 flex items-center justify-center text-xl font-semibold opacity-70 hover:opacity-100 hover:bg-black hover:bg-opacity-10 hover:text-white"
                    >
                        ×
                    </button>
                </div>

                <div className="flex-1 flex flex-col p-4 overflow-hidden">
                    <div className="flex-1 overflow-y-auto">
                        {cartItems.map((item) => (
                            <CartItem key={item.id} {...item} />
                        ))}
                    </div>

                    <div className="pt-4 mt-auto border-t-2 border-gray-200 flex justify-between items-center">
                        <span className="text-xl font-medium text-gray-700">Всего:</span>
                        <span className="text-2xl font-bold text-orange-400">
                            {formatCurrency(totalAmount)}
                        </span>
                    </div>

                    {/* Заглушка */}
                    <button className="mt-7 w-[75%] h-[50px] self-center text-xl text-white bg-stone-900">
                        <Link to="/account">Оформить заказ</Link>
                    </button>
                </div>
            </div>
        </div>
    );
}
