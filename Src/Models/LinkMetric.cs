using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShortenerApi.Models;

public class LinkMetric
{
    public Guid Id { get; set; } =   Guid.NewGuid();
    public string IpAddress { get; set; } = string.Empty;
    public string UserAgent { get; set; } = string.Empty;
    public DateTime AccessedAt { get; set; } =  DateTime.UtcNow;
    public Guid LinkId { get; set; }
    
    public Link? Link { get; set; }
}