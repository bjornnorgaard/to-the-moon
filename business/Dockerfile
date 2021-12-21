FROM golang:1.17-alpine3.15 AS build

WORKDIR /app

COPY . . 

RUN go get

ENV GOOS=linux
ENV GOARCH=amd64
RUN go build main.go

FROM alpine:3.15

WORKDIR /app

COPY --from=build /app/main .

CMD [ "./main" ]