using Microsoft.AspNetCore.Components;

namespace ASPNETCoreMVCTraining.Application.ViewModels
{
    public class PageInfo
    {
        public int PageIndex { get; set; }

        public int TotalPages { get; set; }

        public int TotalRecords { get; set; }

        public int PageSize { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public Dictionary<string,string> Params { get; set; }
    }
}
