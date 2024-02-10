namespace SeatGeek.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SeatGeek.Data.Models;
    using static SeatGeek.Common.EntityValidationConstants;

    public class ParentCategoryEntityValidations : IEntityTypeConfiguration<ParentCategory>
    {
        public void Configure(EntityTypeBuilder<ParentCategory> builder)
        {
            builder.HasData(this.GenerateParentCategories());
        }

        private ParentCategory[] GenerateParentCategories()
        {
            ICollection<ParentCategory> parentCategories = new HashSet<ParentCategory>();

            ParentCategory category;

            category = new ParentCategory()
            {
                Id = 1,
                Name = "Music"

            };
            parentCategories.Add(category);

            category = new ParentCategory()
            {
                Id = 2,
                Name = "Sport"

            };
            parentCategories.Add(category);

            category = new ParentCategory()
            {
                Id = 3,
                Name = "Theatre"

            };
            parentCategories.Add(category);



            return parentCategories.ToArray();
        }
    }
}
