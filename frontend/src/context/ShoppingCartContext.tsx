/* eslint-disable react-refresh/only-export-components */
import React, { createContext, useContext, useState, useEffect } from "react";
import { ShoppingCart } from "../components/ShoppingCart";

type ShoppingCartProviderProps = {
    children: React.ReactNode;
};

type CartItem = {
    id: number;
    quantity: number;
};

type ShoppingCartContextType = {
    isOpen: boolean;
    openCart: () => void;
    closeCart: () => void;
    getItemQuantity: (id: number) => number;
    increaseCartQuantity: (id: number) => void;
    decreaseCartQuantity: (id: number) => void;
    removeFromCart: (id: number) => void;
    cartQuantity: number;
    cartItems: CartItem[];
};

const ShoppingCartContext = createContext<ShoppingCartContextType | undefined>(undefined);

export function useShoppingCart() {
    const context = useContext(ShoppingCartContext);
    console.log("[useShoppingCart] Вызов хука, контекст:", context ? "найден" : "НЕ НАЙДЕН!");

    if (!context) {
        console.error("[useShoppingCart] ОШИБКА: Контекст не найден!");
        console.error("[useShoppingCart] Убедитесь, что компонент обернут в ShoppingCartProvider");
        throw new Error("useShoppingCart must be used within ShoppingCartProvider");
    }

    console.log("[useShoppingCart] Контекст содержит:", {
        cartItems: context.cartItems,
        cartQuantity: context.cartQuantity,
        isOpen: context.isOpen,
    });

    return context;
}

export function ShoppingCartProvider({ children }: ShoppingCartProviderProps) {
    console.log("[ShoppingCartProvider] Монтирование провайдера");

    const [isOpen, setIsOpen] = useState(false);
    const [cartItems, setCartItems] = useState<CartItem[]>(() => {
        const savedCart = localStorage.getItem("shopping-cart");
        console.log(
            "[ShoppingCartProvider] Чтение из localStorage, ключ 'shopping-cart':",
            savedCart
        );

        if (savedCart) {
            try {
                const parsed = JSON.parse(savedCart);
                console.log("[ShoppingCartProvider] Успешно распарсено:", parsed);
                return parsed;
            } catch (error) {
                console.error("[ShoppingCartProvider] Ошибка парсинга localStorage:", error);
                return [];
            }
        }

        console.log("[ShoppingCartProvider] В localStorage ничего нет, начальное значение: []");
        return [];
    });

    console.log("[ShoppingCartProvider] Текущее состояние cartItems:", cartItems);

    useEffect(() => {
        console.log("[ShoppingCartProvider] useEffect: сохранение в localStorage", cartItems);
        try {
            localStorage.setItem("shopping-cart", JSON.stringify(cartItems));
            console.log("[ShoppingCartProvider] Успешно сохранено в localStorage");
        } catch (error) {
            console.error("[ShoppingCartProvider] Ошибка сохранения в localStorage:", error);
        }
    }, [cartItems]);

    const cartQuantity = cartItems.reduce((quantity, item) => {
        const total = item.quantity + quantity;
        console.log(
            `[cartQuantity] Суммирование: item.id=${item.id}, quantity=${item.quantity}, накопленная сумма=${total}`
        );
        return total;
    }, 0);

    console.log("[ShoppingCartProvider] Вычисленная cartQuantity:", cartQuantity);

    const openCart = () => {
        console.log("[openCart] Вызов функции открытия корзины");
        console.log("[openCart] Текущее isOpen:", isOpen, "будет установлено в true");
        setIsOpen(true);
    };

    const closeCart = () => {
        console.log("[closeCart] Вызов функции закрытия корзины");
        console.log("[closeCart] Текущее isOpen:", isOpen, "будет установлено в false");
        setIsOpen(false);
    };

    function getItemQuantity(id: number) {
        const quantity = cartItems.find((item) => item.id === id)?.quantity || 0;
        console.log(`[getItemQuantity] Запрос количества для id=${id}, результат=${quantity}`);
        return quantity;
    }

    function increaseCartQuantity(id: number) {
        console.log(`[increaseCartQuantity] Увеличение количества для id=${id}`);
        console.log(`[increaseCartQuantity] Текущие cartItems до изменения:`, cartItems);

        setCartItems((currItems) => {
            const existingItem = currItems.find((item) => item.id === id);
            console.log(`[increaseCartQuantity] Найден существующий элемент:`, existingItem);

            if (existingItem == null) {
                const newItems = [...currItems, { id, quantity: 1 }];
                console.log(
                    `[increaseCartQuantity] Элемент не найден, добавляем новый. Новые cartItems:`,
                    newItems
                );
                return newItems;
            } else {
                const newItems = currItems.map((item) => {
                    if (item.id === id) {
                        const newItem = { ...item, quantity: item.quantity + 1 };
                        console.log(
                            `[increaseCartQuantity] Увеличиваем количество для id=${id} с ${item.quantity} до ${newItem.quantity}`
                        );
                        return newItem;
                    }
                    return item;
                });
                console.log(
                    `[increaseCartQuantity] Увеличен существующий элемент. Новые cartItems:`,
                    newItems
                );
                return newItems;
            }
        });
    }

    function decreaseCartQuantity(id: number) {
        console.log(`[decreaseCartQuantity] Уменьшение количества для id=${id}`);
        console.log(`[decreaseCartQuantity] Текущие cartItems до изменения:`, cartItems);

        setCartItems((currItems) => {
            const existingItem = currItems.find((item) => item.id === id);
            console.log(`[decreaseCartQuantity] Найден существующий элемент:`, existingItem);

            if (existingItem?.quantity === 1) {
                const newItems = currItems.filter((item) => item.id !== id);
                console.log(
                    `[decreaseCartQuantity] Количество было 1, удаляем элемент. Новые cartItems:`,
                    newItems
                );
                return newItems;
            } else {
                const newItems = currItems.map((item) => {
                    if (item.id === id) {
                        const newItem = { ...item, quantity: item.quantity - 1 };
                        console.log(
                            `[decreaseCartQuantity] Уменьшаем количество для id=${id} с ${item.quantity} до ${newItem.quantity}`
                        );
                        return newItem;
                    }
                    return item;
                });
                console.log(
                    `[decreaseCartQuantity] Уменьшен существующий элемент. Новые cartItems:`,
                    newItems
                );
                return newItems;
            }
        });
    }

    function removeFromCart(id: number) {
        console.log(`[removeFromCart] Удаление элемента id=${id}`);
        console.log(`[removeFromCart] Текущие cartItems до изменения:`, cartItems);

        setCartItems((currItems) => {
            const newItems = currItems.filter((item) => item.id !== id);
            console.log(`[removeFromCart] Удален элемент. Новые cartItems:`, newItems);
            return newItems;
        });
    }

    const contextValue: ShoppingCartContextType = {
        isOpen,
        openCart,
        closeCart,
        getItemQuantity,
        increaseCartQuantity,
        decreaseCartQuantity,
        removeFromCart,
        cartItems,
        cartQuantity,
    };

    console.log("[ShoppingCartProvider] Создаваемое значение контекста:", contextValue);

    return (
        <ShoppingCartContext.Provider value={contextValue}>
            {children}
            <ShoppingCart />
        </ShoppingCartContext.Provider>
    );
}
