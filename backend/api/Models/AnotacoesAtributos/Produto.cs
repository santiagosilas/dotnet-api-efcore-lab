using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace api.Models;

[Table("Produtos")]
public class Produto
{

    [Key]
    public long Codigo { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3, ErrorMessage = "O nome deve ter pelo menos 3 caracteres")]
    [MaxLength(50, ErrorMessage = "O nome não pode exceder 50 caracteres")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "A descrição é obrigatória")]
    [StringLength(200, ErrorMessage = "A descrição não pode exceder 200 caracteres")]
    public string? Descricao { get; set; }
    public DateOnly DataDeValidade { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.Now;
    public DateTime AtualizadoEm { get; set; } = DateTime.Now;
    public bool EstaEsgotado { get; set; }

    public int QuantidadeEmEstoque { get; set; }

    public float PesoEmKg { get; set; }

    public string? ImagemUrl { get; set; }

    [Range(0.01, 100.00, ErrorMessage = "O preço deve estar entre 0.01 e 100.00")]
    [Precision(18, 2)] // 18 dígitos totais, 2 casas decimais
    public decimal Preco { get; set; }
}