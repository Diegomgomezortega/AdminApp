using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AdminApp.Models;
public class Category
{
    //DataAnotation: No combinar con Fluent Api
    // [Key]
    public Guid CategoryId { get; set; }
    // [Required]
    // [MaxLength(30)]
    public string Name { get; set; }
    // [Required]
    // [MaxLength(150)]
    public string Description { get; set; }

    public double AmountAllowed { get; set; }
    

    //Agrego esta anotación para no generar un circulo entre Transacciones y Categorias
    [JsonIgnore]
    public virtual ICollection<Transaccion> TransactionsList { get; set; }

}