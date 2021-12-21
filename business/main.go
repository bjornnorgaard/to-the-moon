package main

import (
	"business/counter"
	"github.com/gin-gonic/gin"
	"os"
)

func main() {
	r := gin.Default()

	counterBaseUrl := os.Getenv("COUNTER_SERVICE_URL")
	if len(counterBaseUrl) == 0 {
		counterBaseUrl = "http://localhost:5001"
	}
	counterConfig := counter.Config{CountersBaseUrl: counterBaseUrl}

	counter.AddEndpoints(r, &counterConfig)

	err := r.Run(":8080")
	if err != nil {
		panic(err)
	}
}
