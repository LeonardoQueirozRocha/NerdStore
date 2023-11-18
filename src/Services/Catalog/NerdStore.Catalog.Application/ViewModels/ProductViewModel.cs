using System.ComponentModel.DataAnnotations;

namespace NerdStore.Catalog.Application.ViewModels;

public class ProductViewModel
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public Guid CategoryId { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [Display(Name = "Nome")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [Display(Name = "Descrição")]
    public string Description { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [Display(Name = "Ativo")]
    public bool Active { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [Display(Name = "Valor")]
    public decimal Value { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [Display(Name = "Data de Cadastro")]
    public DateTime RegistrationDate { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [Display(Name = "Imagem")]
    public string Image { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "O campo {0} precisa ter o valor mínimo de {1}")]
    [Display(Name = "Quantidade em estoque")]
    public int QuantityInStock { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "O campo {0} precisa ter o valor mínimo de {1}")]
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [Display(Name = "Altura")]
    public int Height { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "O campo {0} precisa ter o valor mínimo de {1}")]
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [Display(Name = "Largura")]
    public int Width { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "O campo {0} precisa ter o valor mínimo de {1}")]
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [Display(Name = "Profundidade")]
    public int Depth { get; set; }

    public IEnumerable<CategoryViewModel> Categories { get; set; }
}
