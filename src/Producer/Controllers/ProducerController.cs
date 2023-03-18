using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;

namespace Producer.Controllers;

[ApiController]
[Route("[controller]")]
public class ProducerController : ControllerBase
{
    private readonly ILogger<ProducerController> _logger;
    private const string _bootstrapServers = "localhost:9092";
    private const string _producerTopic = "example-topic";

    public ProducerController(ILogger<ProducerController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "ProduceMessage")]
    public async Task<bool> Get()
    {
        var config = new ProducerConfig { BootstrapServers = _bootstrapServers };
        
        using var producer = new ProducerBuilder<Null, string>(config).Build();

        try
        {
            await producer.ProduceAsync(_producerTopic, new Message<Null, string> { Value = DateTime.Now.ToString() });
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured.");
        }
        finally
        {
            producer.Flush(TimeSpan.FromSeconds(10));
        }

        return false;
    }
}
