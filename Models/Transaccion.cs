using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AdminApp.Models;

namespace AdminApp.Models;
public class Transaccion
{
    //DataAnotation: No combinar con Fluent Api
    // [Key]
    public Guid TransactionId { get; set; }

    // [ForeignKey("CategoryId")]
    public Guid CategoryId { get; set; }

    // [Required]
    // [MaxLength(100)]
    public string Title { get; set; }
    public string Description { get; set; }

    public TypesTransaction Type { get; set; }

    public DateTime Date { get; set; }

    public virtual Category Category { get; set; }
    
    // [NotMapped]
    public string Resumen { get; set; }    

}
public enum TypesTransaction 
{
    House,
    Pets,
    Clearing,
    Food,
    Clothes,
    Education,
    Event,
    Trip

}