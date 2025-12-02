
namespace Merlebleu.Spent.Data.Configurations;

public class ExpenseConfiguration : IEntityTypeConfiguration<Models.Expense>
{
    public void Configure(EntityTypeBuilder<Models.Expense> builder)
    {
        builder.ToTable("Expenses");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Amount)
            .HasPrecision(18, 2);
    }
}
