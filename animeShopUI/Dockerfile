FROM node:latest AS build
WORKDIR /app
COPY ["package*.json", "frontend/"]
WORKDIR /app/frontend
RUN ls
RUN npm install
COPY . .
CMD ["npm", "start"]
