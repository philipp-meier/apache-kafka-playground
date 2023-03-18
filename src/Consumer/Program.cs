using Confluent.Kafka;

var config = new ConsumerConfig
{
    GroupId = "example-group",
    BootstrapServers = "localhost:9092",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
consumer.Subscribe("example-topic");

var cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };

try
{
    while (true)
    {
        try
        {
            var cr = consumer.Consume(cts.Token);
            Console.WriteLine($"Consumed message \"{cr.Message.Value}\" from topic \"{cr.TopicPartitionOffset}\".");
        }
        catch (ConsumeException ex)
        {
            Console.WriteLine("Error: {0}", ex.Error);
        }
    }
}
catch (OperationCanceledException)
{
    consumer.Close();
}