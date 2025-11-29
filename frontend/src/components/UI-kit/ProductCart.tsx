import { useShoppingCart } from "../../context/ShoppingCartContext";
import { useAuth } from "../../context/AuthContext"; // твой контекст авторизации
import { useNavigate } from "react-router-dom";
import { formatCurrency } from "../../utilities/formatCurrency";

import storeItems from "../../data/items.json"; // временно

type ProductCartProps = {
    id: number;
    name: string;
    price: number;
    imgUrl: string;
};

export function ProductCart({ id, name, price, imgUrl }: ProductCartProps) {
    const { getItemQuantity, increaseCartQuantity, decreaseCartQuantity, removeFromCart } =
        useShoppingCart();
    const { isAuthenticated } = useAuth();
    const navigate = useNavigate();

    const quantity = getItemQuantity(id);

    const handleAddToCart = () => {
        if (!isAuthenticated) {
            navigate("/login");
            return;
        }
        increaseCartQuantity(id);
    };

    return (
        <div className="relative flex flex-col items-start text-center h-[550px] max-w-[520px] border-2 border-gray-300">
            <img
                src="images/chandelier-cart-test.png"
                alt={name}
                className="w-full h-[300px] self-center object-contain relative z-1"
            />

            <div className="w-full relative z-1 flex justify-between items-start mt-5 pl-[30px] pr-[30px]">
                <div className="flex-1 flex flex-col items-start">
                    <h3 className="text-2xl mb-1">Название</h3>
                    <p className="w-[30px] h-[30px] text-gray-400 font-medium text-xl">Модель</p>
                </div>
                <button>
                    <img src="icons/favourite-icon.png" alt="Star icon" />
                </button>
            </div>
            <p className="text-xl ml-[30px] mt-[15px]">{formatCurrency(price)}</p>

            <div className="w-full mt-auto z-10">
                {quantity === 0 ? (
                    isAuthenticated ? (
                        <button
                            onClick={handleAddToCart}
                            className="w-[75%] mb-[5%] py-3 px-4 bg-green-500 text-white text-xl"
                        >
                            Добавить в корзину
                        </button>
                    ) : (
                        <button
                            onClick={() => navigate("/login")}
                            className="w-[75%] mb-[5%] py-3 px-4 bg-green-500 text-white text-xl"
                        >
                            Купить
                        </button>
                    )
                ) : (
                    <div className="pl-[30px] pr-[30px] space-y-2">
                        <div className="flex items-center justify-between">
                            <button
                                onClick={() => decreaseCartQuantity(id)}
                                className="w-[20%] h-10 bg-green-500 text-white text-xl flex items-center justify-center"
                            >
                                —
                            </button>
                            <span className="text-xl flex-1 text-center font-bold">
                                {quantity} шт. в корзине
                            </span>
                            <button
                                onClick={handleAddToCart}
                                className="w-[20%] h-10 bg-green-500 text-white text-xl flex items-center justify-center"
                            >
                                +
                            </button>
                        </div>

                        <button
                            onClick={() => removeFromCart(id)}
                            className="w-full h-10 mb-4 bg-red-400 text-white text-xl flex items-center justify-center"
                        >
                            Удалить из корзины
                        </button>
                    </div>
                )}
            </div>
        </div>
    );
}
