using AdminApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminApp.Data;
public class AdminAppContext: DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Transaccion> Transactions { get; set; }
    
    public AdminAppContext(DbContextOptions<AdminAppContext> options) :base(options) {}


    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        List<Category> CategoryListiInit= new List<Category>();
        CategoryListiInit.Add(new Category(){
                                CategoryId=Guid.Parse("b850770d-270d-4d96-b69e-062030a37d27"),
                                Name="Comida",
                                Description="Gastos Realizados en comida mensual/diaria",
                                AmountAllowed=10000});
        CategoryListiInit.Add(new Category(){
                                CategoryId=Guid.Parse("b850770d-270d-4d96-b69e-062030a37d58"),
                                Name="Mantenimiento Casa",
                                Description="Gastos Realizados Mantenimiento",
                                AmountAllowed=7000});
                                

        List<Transaccion> TransaccionListiInit= new List<Transaccion>();
        TransaccionListiInit.Add(new Transaccion(){
                                TransactionId=Guid.Parse("b850770d-270d-4d96-b69e-062030a37d11"),
                                CategoryId=Guid.Parse("b850770d-270d-4d96-b69e-062030a37d27"),
                                Title="Compra del dino",
                                Description="Compra mensual y asado",
                                Type=TypesTransaction.Food,
                                Date=DateTime.Now                                
                                });
        TransaccionListiInit.Add(new Transaccion(){
                                TransactionId=Guid.Parse("b850770d-270d-4d96-b69e-062030a37d37"),
                                CategoryId=Guid.Parse("b850770d-270d-4d96-b69e-062030a37d58"),
                                Title="Gotita",
                                Description="Reparacion Inodoro",
                                Type=TypesTransaction.House,
                                Date=DateTime.Now                                
                                });
        modelBuilder.Entity<Category>(category =>
        {
            category.ToTable("Category");
            category.HasKey(p=>p.CategoryId);
            category.Property(p=>p.Name).IsRequired().HasMaxLength(30);
            category.Property(p=>p.Description).IsRequired().HasMaxLength(150);
            category.Property(p=>p.AmountAllowed);
            category.HasData(CategoryListiInit);

        });

         modelBuilder.Entity<Transaccion>(transaction =>
        {
            transaction.ToTable("Transaction");
            transaction.HasKey(p=>p.TransactionId);
            transaction.HasOne(p=>p.Category).WithMany(p=>p.TransactionsList).HasForeignKey(p=>p.CategoryId);
            transaction.Property(p=>p.Title).IsRequired().HasMaxLength(100);
            transaction.Property(p=>p.Description).IsRequired().HasMaxLength(150);
            transaction.Property(p=>p.Date);
            transaction.Property(p=>p.Type);
            transaction.Property(p=>p.Resumen).IsRequired(false);
            

            transaction.HasOne(p=>p.Category).WithMany(p=>p.TransactionsList).HasForeignKey(p=>p.CategoryId);
            //En versiones anteriores, si queremos que no se cree
            //una tabla de un atributo, se hace lo siguiente:
            // transaction.Ignore(p=>p.Resumen);

            transaction.HasData(TransaccionListiInit);
            

        });

    }

}