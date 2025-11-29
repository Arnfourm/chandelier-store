import { useShoppingCart } from "../context/ShoppingCartContext";
import { formatCurrency } from "../utilities/formatCurrency";

import storeItems from "../data/items.json"; // временно

type CartItemProps = {
    id: number;
    quantity: number;
};

export function CartItem({ id, quantity }: CartItemProps) {
    const { removeFromCart } = useShoppingCart();
    const item = storeItems.find((i) => i.id === id);

    if (item == null) return null;

    return (
        <div className="grid grid-cols-[80px_1fr_auto] gap-4 items-center p-4 border-b border-gray-200 last:border-b-0 hover:bg-gray-50 transition-colors">
            <img src={item.imgUrl} alt={item.name} className="w-[70px] h-[70px] object-contain" />

            <div className="flex flex-col gap-2">
                <div className="font-semibold text-lg text-gray-900">
                    {item.name}{" "}
                    {quantity > 1 && (
                        <span className="text-sm text-gray-500 font-medium">×{quantity}</span>
                    )}
                </div>
                <div className="flex flex-col gap-1 text-sm">
                    <div className="text-gray-500">{formatCurrency(item.price)} за шт.</div>
                    <div className="font-semibold text-gray-900 text-base">
                        {formatCurrency(item.price * quantity)}
                    </div>
                </div>
            </div>

            <button
                onClick={() => removeFromCart(id)}
                className="w-6 h-6 bg-red-400 text-white flex items-center justify-center text-sm font-bold"
            >
                ×
            </button>
        </div>
    );
}
