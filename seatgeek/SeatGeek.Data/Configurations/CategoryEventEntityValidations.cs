namespace SeatGeek.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SeatGeek.Data.Models;
    public class CategoryEventEntityValidations : IEntityTypeConfiguration<CategoryEvent>
    {
        public void Configure(EntityTypeBuilder<CategoryEvent> builder)
        {
            builder
                .HasKey(x => new { x.ChildCategoryId, x.EventId });



        }
    }
}
