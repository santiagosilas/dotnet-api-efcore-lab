
using api.DTOs;

public class ContatoComEnderecoDTO
{
    public long Id { get; set; }
    public string? Nome { get; set; }

    public EnderecoDTO Endereco { get; set; }

}
