
using api.DTOs;

public record ImagemDTOUpdate
{
    public long Id { get; set; }
    public string? Titulo { get; set; } = string.Empty;

}

