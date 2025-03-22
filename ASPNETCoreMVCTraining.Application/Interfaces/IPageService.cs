using System.Dynamic;
using ASPNETCoreMVCTraining.Application.ViewModels;

namespace ASPNETCoreMVCTraining.Application.Interfaces;

public interface IPageService
{
    ExpandoObject GetPageParams(int pageIndex, PageInfo pageInfo);
}