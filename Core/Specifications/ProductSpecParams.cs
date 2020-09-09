namespace Core.Specifications
{
    //ProductControllerda metodların aldığı parametreleri çok fazla olduğu için sadece parametleri içeren sınıf oluşturduk.
    public class ProductSpecParams
    {
        private const int MaxPageSize = 50;
        public int PageIndex {get; set;} = 1;
        private int _pageSize = 6;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value; // gelen value 50 den büyükse 50'nin kendisini seç değilse girilen value seç.
        }

        public int? BrandId {get; set;}
        public int? TypeId {get; set;}
        public string Sort {get; set;}
        // isme göre item getirmek için önce kullanıcın girdiği product ismini lower yapıyoruz.
        private string _search;
        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }

    }
}