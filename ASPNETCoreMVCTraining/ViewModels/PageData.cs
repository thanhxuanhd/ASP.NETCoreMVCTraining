namespace ASPNETCoreMVCTraining.ViewModels;

public class PageData<T> where T : class
{
    public IEnumerable<T> Data { get; set; } = new List<T>();

    public PageInfo PageInfo { get; set; }
}
