FROM node:22 AS build
WORKDIR /app/
EXPOSE 5173

COPY ["./package.json", "./frontend/"]
COPY ["./package-lock.json", "./frontend/"]

RUN npm install ./frontend/

COPY . ./frontend/

WORKDIR /app/frontend/
RUN npm run build -c Release

FROM nginx:alpine3.22 as FINAL
COPY ["./nginx.conf", "/etc/nginx/conf.d/default.conf"]
COPY --from=build /app/frontend/dist /app/dist/
