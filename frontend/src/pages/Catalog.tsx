import { Footer } from "../components/Footer";
import { Header } from "../components/Header";
import { ProductCart } from "../components/UI-kit/ProductCart";

const products = async () => {
    try {
        const res = await fetch("http://localhost:9220/api/Catalog/Product", {
            method: "GET",
            headers: {
                accept: "text/plain",
            },
        });

        if (res.ok) {
            console.log(await res.text());
        }
    } catch (err) {
        console.log(err);
    }
};

export function Catalog() {
    products();

    return (
        <>
            <Header />

            <div className="mt-10 mb-10 pl-[100px] pr-[100px] flex justify-between">
                <h1 className="uppercase text-5xl ">Каталог</h1>
                <p className="w-[200px] text-xl text-wrap">Мы сделаем вашу жизнь светлее!</p>
            </div>

            <div className="h-[1500px] flex">
                <section className="w-[500px] h-full border border-l-0 border-gray-300">
                    <div className="w-full h-20 pr-[30px] pl-[100px] flex justify-between items-center border-b border-gray-300">
                        <h2 className="text-2xl">Фильтры</h2>
                        <div>
                            <button>
                                <img
                                    src="icons/save-filter-icon.png"
                                    alt="Save filters icon"
                                    className="w-7 h-7 mr-2.5"
                                />
                            </button>
                            <button>
                                <img
                                    src="icons/reset-icon.png"
                                    alt="Reset icon"
                                    className="w-[30px] h-[30px]"
                                />
                            </button>
                        </div>
                    </div>
                    <section className="w-full h-60 pl-[100px] pr-10 flex flex-col justify-center gap-y-2.5 border-b border-gray-300 text-xl">
                        <h3 className="mb-5">Тип помещения</h3>
                        <div className="w-full flex justify-between">
                            <label>Встроенный</label>
                            <input type="checkbox" className="w-5 h-5 accent-amber-400" />
                        </div>
                        <div className="w-full flex justify-between">
                            <label>Накладной</label>
                            <input type="checkbox" className="w-5 h-5 accent-amber-400" />
                        </div>
                        <div className="w-full flex justify-between">
                            <label>Подвесной</label>
                            <input type="checkbox" className="w-5 h-5 accent-amber-400" />
                        </div>
                    </section>
                    <section className="w-full h-60 pl-[100px] pr-10 flex flex-col justify-center gap-y-2.5 border-b border-gray-300 text-xl">
                        <h3 className="mb-5">Основной цвет</h3>
                        <div className="w-full flex justify-between">
                            <label>Белый</label>
                            <input type="checkbox" className="w-5 h-5 accent-amber-400" />
                        </div>
                        <div className="w-full flex justify-between">
                            <label>Черный</label>
                            <input type="checkbox" className="w-5 h-5 accent-amber-400" />
                        </div>
                        <div className="w-full flex justify-between">
                            <label>Золотой</label>
                            <input type="checkbox" className="w-5 h-5 accent-amber-400" />
                        </div>
                    </section>
                    <section className="h-[180px] pl-[100px] flex flex-col justify-center border-b border-gray-300 text-xl">
                        <h3 className="mb-5">Мощность, Вт</h3>
                        <div className="flex">
                            <input
                                placeholder="24"
                                className="w-[40%] h-[45px] border border-gray-300 text-center"
                            />
                            <input
                                placeholder="40"
                                className="w-[40%] h-[45px] border border-gray-300 text-center"
                            />
                        </div>
                    </section>
                    <section className="h-[180px] pl-[100px] flex flex-col justify-center border-b border-gray-300 text-xl">
                        <h3 className="mb-5">Количество ламп, шт.</h3>
                        <div className="flex">
                            <input
                                placeholder="1"
                                className="w-[40%] h-[45px] border border-gray-300 text-center"
                            />
                            <input
                                placeholder="100"
                                className="w-[40%] h-[45px] border border-gray-300 text-center"
                            />
                        </div>
                    </section>
                    <section className="h-[180px] pl-[100px] flex flex-col justify-center text-xl">
                        <h3 className="mb-5">Цена, руб.</h3>
                        <div className="flex">
                            <input
                                placeholder="0"
                                className="w-[40%] h-[45px] border border-gray-300 text-center"
                            />
                            <input
                                placeholder="1000000"
                                className="w-[40%] h-[45px] border border-gray-300 text-center"
                            />
                        </div>
                    </section>
                </section>

                <section className="w-full h-full flex flex-col justify-between">
                    <section className="w-full h-20 flex border border-gray-300">
                        <div className="w-[275px] flex justify-center items-center border-r border-gray-300">
                            <label>Сортировка:</label>
                            <select name="sorting" id="cart-select">
                                <option value="">отсутствует</option>
                                <option value="price h">по цене &#9660;</option>
                                <option value="price">по цене &#9650;</option>
                                <option value="date h">по дате &#9660;</option>
                                <option value="date">по дате &#9650;</option>
                            </select>
                        </div>
                        <div className="w-full h-full pl-10 flex items-center">
                            <img
                                src="icons/search-icon.png"
                                alt="Search icon"
                                className="w-[30px] h-[30px] mr-5"
                            />
                            <input
                                type="text"
                                placeholder="Введите ваш поисковой запрос..."
                                className="w-full h-20 p-5 text-gray-400"
                            />
                        </div>
                    </section>
                    <section>
                        <ProductCart
                            id={1}
                            name="LED Светильник"
                            price={2500}
                            imgUrl="path/to/image.jpg"
                        />
                    </section>
                    <section className="w-full h-20 pl-[100px] pr-[100px] flex justify-between items-center border border-gray-300 text-2xl">
                        <button className="text-4xl text-gray-400">&#8249;</button>
                        <button className="text-amber-500">1</button>
                        <button className="text-4xl text-amber-500">&#8250;</button>
                    </section>
                </section>
            </div>
            <Footer />
        </>
    );
}
