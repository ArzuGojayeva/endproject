namespace Task.ViewModels
{
    public class PaginateVM<T>
    {
        public List<T> Itens { get; set; }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }

    }
}
