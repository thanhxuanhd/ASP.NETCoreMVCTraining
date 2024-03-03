using System.Dynamic;
using ASPNETCoreMVCTraining.ViewModels;

namespace ASPNETCoreMVCTraining.Interfaces;

public interface IPageService
{
    ExpandoObject GetPageParams(int pageIndex, PageInfo pageInfo);
}