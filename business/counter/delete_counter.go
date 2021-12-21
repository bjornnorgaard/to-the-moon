package counter

import (
	"bytes"
	"encoding/json"
	"fmt"
	"github.com/gin-gonic/gin"
	"net/http"
)

type deleteCounterRequest struct {
	CounterId string `json:"counterId"`
}

func deleteCounter(config *Config) gin.HandlerFunc {
	return func(c *gin.Context) {
		var req deleteCounterRequest
		err := c.ShouldBindJSON(&req)
		if err != nil {
			c.String(http.StatusBadRequest, err.Error())
			return
		}

		err = deleteCounterHandler(config, &req)

		if err != nil {
			c.String(http.StatusInternalServerError, err.Error())
			return
		}

		c.Status(http.StatusAccepted)
		return
	}
}

func deleteCounterHandler(config *Config, req *deleteCounterRequest) error {
	requestUrl := fmt.Sprintf("%s/counters/delete-counter", config.CountersBaseUrl)

	marshalled, err := json.Marshal(req)
	if err != nil {
		return err
	}

	reader := bytes.NewReader(marshalled)
	_, err = http.Post(requestUrl, "application/json", reader)
	if err != nil {
		return err
	}

	return nil
}
