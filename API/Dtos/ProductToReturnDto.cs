namespace API.Dtos
{
    public class ProductToReturnDto
    {
        // geri dönen verimiz bu şekli alacak
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string ProductType { get; set; }
        public string ProductBrand { get; set; }
    
    }
}