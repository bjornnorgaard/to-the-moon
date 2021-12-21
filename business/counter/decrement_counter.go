package counter

import (
	"bytes"
	"encoding/json"
	"fmt"
	"github.com/gin-gonic/gin"
	"net/http"
)

type decrementCounterRequest struct {
	CounterId string `json:"counterId"`
}

type decrementCounterResponse struct {
	UpdatedCounter struct {
		Id    string `json:"id"`
		Name  string `json:"name"`
		Value int    `json:"value"`
	} `json:"updatedCounter"`
}

func decrementCounter(config *Config) gin.HandlerFunc {
	return func(c *gin.Context) {
		var req decrementCounterRequest
		err := c.ShouldBindJSON(&req)
		if err != nil {
			c.String(http.StatusBadRequest, err.Error())
			return
		}

		res, err := decrementCounterHandler(config, &req)

		if err != nil {
			c.String(http.StatusInternalServerError, err.Error())
			return
		}

		c.JSON(http.StatusOK, res)
		return
	}
}

func decrementCounterHandler(config *Config, req *decrementCounterRequest) (*decrementCounterResponse, error) {
	requestUrl := fmt.Sprintf("%s/counters/decrement-counter", config.CountersBaseUrl)

	marshalled, err := json.Marshal(req)
	if err != nil {
		return nil, err
	}

	reader := bytes.NewReader(marshalled)
	response, err := http.Post(requestUrl, "application/json", reader)
	if err != nil {
		return nil, err
	}

	defer response.Body.Close()

	var getCounterResponse decrementCounterResponse
	err = json.NewDecoder(response.Body).Decode(&getCounterResponse)
	if err != nil {
		return nil, err
	}

	return &getCounterResponse, nil
}
