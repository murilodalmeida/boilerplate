FROM node:16 AS build

WORKDIR /app

COPY ./frontend/validation-tests/package*.json ./

RUN npm install

COPY ./frontend/validation-tests .

RUN npm run build --prod

FROM nginx:alpine

COPY --from=build /app/dist/validation-tests /usr/share/nginx/html

EXPOSE 80