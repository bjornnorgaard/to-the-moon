package counter

import (
	"bytes"
	"encoding/json"
	"fmt"
	"github.com/gin-gonic/gin"
	"net/http"
)

type createCounterRequest struct {
	Name string `json:"name"`
}

type createCounterResponse struct {
	CreatedCounter struct {
		Id    string `json:"id"`
		Name  string `json:"name"`
		Value int    `json:"value"`
	} `json:"createdCounter"`
}

func createCounter(config *Config) gin.HandlerFunc {
	return func(c *gin.Context) {
		var req createCounterRequest
		err := c.ShouldBindJSON(&req)
		if err != nil {
			c.String(http.StatusBadRequest, err.Error())
			return
		}

		res, err := createCounterHandler(config, &req)

		if err != nil {
			c.String(http.StatusInternalServerError, err.Error())
			return
		}

		c.JSON(http.StatusOK, res)
		return
	}
}

func createCounterHandler(config *Config, req *createCounterRequest) (*createCounterResponse, error) {
	requestUrl := fmt.Sprintf("%s/counters/create-counter", config.CountersBaseUrl)

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

	var getCounterResponse createCounterResponse
	err = json.NewDecoder(response.Body).Decode(&getCounterResponse)
	if err != nil {
		return nil, err
	}

	return &getCounterResponse, nil
}
