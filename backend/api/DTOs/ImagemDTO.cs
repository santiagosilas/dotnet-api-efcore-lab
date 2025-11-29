
using api.DTOs;

public record ImagemDTO
{
    public long Id { get; set; }
    public string? Titulo { get; set; } = string.Empty;
    public string? Url { get; set; } = string.Empty;

}

