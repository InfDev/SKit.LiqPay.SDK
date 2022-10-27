namespace BlazorApp.Models
{
    public class TestCard
    {
        public TestCard(string number, string descriptionEn, string descriptionUk)
        {
            Number = number;
            DescriptionEn = descriptionEn;
            DescriptionUk = descriptionUk;
        }

        public string Number { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionUk { get; set; }

        public string Description(string language) => language == "uk" ? DescriptionUk : DescriptionEn;
        public string DisplayName(string language) => $"{Number} ⇒ {Description(language)}";
    }
}
