# Веб-приложение для магазина "Империя люстр"

Курсовой проект по дисциплине «Технологии программирования» (ВлГУ) — интернет-магазин осветительных приборов «Империя люстр». Веб-приложение с микросервисной backend-архитектурой, клиентским приложением и инфраструктурой для развертывания.

> Разработано в учебных целях Нямаа Артуром и Манайчевой Викторией

## Структура проекта

```
/
├── automate/                        # Автоматизация развертки приложений
├── microservices/                   # Микросервисы, backend-часть
│   ├── microservices.sln
│   ├── microservices.ApiGateway/    # API-шлюз, единая точка входа
│   ├── microservices.AnalyzeAPI/    # Микросервис аналитики
│   ├── microservices.SupplyAPI/     # Микросервис управления поставками
│   ├── microservices.CatalogAPI/    # Микросервис каталога товаров
│   ├── microservices.OrderAPI/      # Микросервис управления заказами
│   ├── microservices.ReviewAPI/     # Микросервис управления отзывами
│   ├── microservices.UserAPI/       # Микросервис управления пользователями
│   └── TestMicroservices/           # Отдельный проект для тестирования
│       └── UnitTests/               # Модульные (unit) тесты
├── frontend/
│   ├── public/                      # Статические ресурсы
│   ├── src/
│   │   ├── components/              # React-компоненты
│   │   ├── pages/                   # React-компоненты страниц
│   │   ├── context/                 # React Context для глобального состояния
│   │   ├── hooks/                   # Кастомные React-хуки
│   │   ├── types/                   # TypeScript-типы и интерфейсы
│   │   ├── utilites/                # Вспомогательные функции и утилиты
│   │   ├── index.css
│   │   ├── App.tsx                  # Корневой компонент приложения
│   │   └── main.tsx                 # Точка входа
│   ├── vite.config.ts               # Конфигурация сборщика Vite
│   ├── package.json                 # Конфигурация зависимостей
│   └── index.html
├── docs/                            # Документация по курсовому проекту
├── docker-compose.yml               # Конфигурация Docker Compose
├── nginx.conf                       # Конфигурация Nginx
└── README.md                        # Документация к приложению
```

## Стек технологий

-   **Backend:** ASP.NET (C#)
-   **Frontend:** React, TypeScript, TailwindCSS, Vite
-   **База данных:** PostgreSQL
-   **Деплой:** Docker, Nginx
-   **Тестирование:** NUnit, Moq

## Установка и запуск

1.
