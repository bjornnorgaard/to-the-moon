package counter

import "github.com/gin-gonic/gin"

func AddEndpoints(r *gin.Engine, c *Config) {
	r.POST("counters/get-counters", getCounters(c))
	r.POST("counters/increment-counter", incrementCounter(c))
	r.POST("counters/decrement-counter", decrementCounter(c))
	r.POST("counters/create-counter", createCounter(c))
	r.POST("counters/delete-counter", deleteCounter(c))
}
