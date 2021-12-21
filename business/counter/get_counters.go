package counter

import (
	"bytes"
	"encoding/json"
	"fmt"
	"github.com/gin-gonic/gin"
	"net/http"
)

type getCountersRequest struct {
	PageNumber int `json:"pageNumber"`
	PageSize   int `json:"pageSize"`
}

type getCountersResponse struct {
	Counters []struct {
		Id    string `json:"id"`
		Name  string `json:"name"`
		Value int    `json:"value"`
	} `json:"counters"`
}

func getCounters(config *Config) gin.HandlerFunc {
	return func(c *gin.Context) {
		var req getCountersRequest
		err := c.ShouldBindJSON(&req)
		if err != nil {
			c.String(http.StatusBadRequest, err.Error())
			return
		}

		res, err := getCountersHandle(config, &req)

		if err != nil {
			c.String(http.StatusInternalServerError, err.Error())
			return
		}

		c.JSON(http.StatusOK, res)
		return
	}
}

func getCountersHandle(config *Config, req *getCountersRequest) (*getCountersResponse, error) {
	requestUrl := fmt.Sprintf("%s/counters/get-counters", config.CountersBaseUrl)

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

	var getCounterResponse getCountersResponse
	err = json.NewDecoder(response.Body).Decode(&getCounterResponse)
	if err != nil {
		return nil, err
	}

	return &getCounterResponse, nil
}
