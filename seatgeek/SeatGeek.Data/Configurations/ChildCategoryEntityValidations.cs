namespace SeatGeek.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SeatGeek.Data.Models;
    using static SeatGeek.Common.EntityValidationConstants;

    public class ChildCategoryEntityValidations : IEntityTypeConfiguration<ChildCategory>
    {
        public void Configure(EntityTypeBuilder<ChildCategory> builder)
        {
            builder.HasData(this.GenerateChildCategories());
        }

        private ChildCategory[] GenerateChildCategories()
        {
            ICollection<ChildCategory> childCategories = new HashSet<ChildCategory>();

            ChildCategory category;

            category = new ChildCategory()
            {
                Id = 1,
                Name = "Pop",
                ParentCategoryId = 1,


            };
            childCategories.Add(category);

            category = new ChildCategory()
            {
                Id = 2,
                Name = "Rock",
                ParentCategoryId = 1,


            };
            childCategories.Add(category);

            category = new ChildCategory()
            {
                Id = 3,
                Name = "Football",
                ParentCategoryId = 2,


            };
            childCategories.Add(category);




            return childCategories.ToArray();
        }

    }
}
