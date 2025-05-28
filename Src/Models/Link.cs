using System.ComponentModel.DataAnnotations;

namespace ShortenerApi.Models;

public class Link
{
    public Guid Id { get; set; } =  Guid.NewGuid();
    public string OriginalUrl { get; set; } = string.Empty;
    public string ShortCode { get; set; } = string.Empty;
    public int Clicks { get; set; }
    
    public ICollection<LinkMetric> Metrics { get; set; } = new List<LinkMetric>();
}