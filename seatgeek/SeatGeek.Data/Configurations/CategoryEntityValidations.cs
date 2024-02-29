namespace SeatGeek.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SeatGeek.Data.Models;
    using static SeatGeek.Common.EntityValidationConstants;

    public class CategoryEntityValidations : IEntityTypeConfiguration<Models.Category>
    {
        public void Configure(EntityTypeBuilder<Models.Category> builder)
        {
            builder.HasData(this.GenerateCategories());
        }

        private Models.Category[] GenerateCategories()
        {
            ICollection<Models.Category> Categories = new HashSet<Models.Category>();

            Models.Category category;

            category = new Models.Category()
            {
                Id = 1,
                Name = "Music",
                


            };
            Categories.Add(category);

            category = new Models.Category()
            {
                Id = 2,
                Name = "Sport",
              


            };
            Categories.Add(category);

            category = new Models.Category()
            {
                Id = 3,
                Name = "Theatre",
                


            };
            Categories.Add(category);




            return Categories.ToArray();
        }

    }
}
