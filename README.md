# Resiliency.Demo
[![Generic badge](https://img.shields.io/badge/.NET-5.0-purple.svg)](https://dotnet.microsoft.com/download/dotnet/5.0)
[![Generic badge](https://img.shields.io/badge/Polly-7.2-green.svg)](https://github.com/App-vNext/Polly)
[![Generic badge](https://img.shields.io/badge/Stringer-1.0.1.0-blue.svg)](https://github.com/ericserafim/Stringer)

Microservice Resiliency Example

This example is composed of two APIs to simulate a dependency between microservices.

#### API
[API microservice](https://github.com/ericserafim/Resiliency.Demo/tree/master/Api) is using [Polly](https://github.com/App-vNext/Polly) to provide HTTP call resiliency. It's applying retrying and circuit-breaker.

#### External API
[External API microservice](https://github.com/ericserafim/Resiliency.Demo/tree/master/ExternalApi) is a simple weather API with a Failing middleware to keep the service down or up.
For example:
* To enable failing responses call: GET https://localhost:7001/failing?enable
* To disable failing responses call: GET https://localhost:7001/failing?disable

With failing middleware enabled, all responses will be 500.


## Running locally
Considering that you have Docker already installed. Just run the following command:

```
docker-compose up
```
