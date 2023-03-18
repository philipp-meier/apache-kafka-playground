# .NET Apache Kafka playground
Playground for testing the [Apache Kafka](https://kafka.apache.org/) event streaming platform using [Confluent's Apache Kafka .NET client](https://github.com/confluentinc/confluent-kafka-dotnet).

## Getting started
To get started, run `docker compose up -d` in the main directory, to start Apache Kafka. After that, you  can run the `Consumer` and `Producer` with `dotnet run` in the corresponding project directory.

To add a message to the queue, go to `https://localhost:7060/Producer` or trigger the producer via the Swagger-UI: `https://localhost:7060/swagger`.
