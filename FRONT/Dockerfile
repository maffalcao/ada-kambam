FROM node:14

WORKDIR /app

COPY FRONT/package.json ./

RUN npm install

COPY FRONT/src/. .

EXPOSE 5001

CMD [ "npm", "start" ]