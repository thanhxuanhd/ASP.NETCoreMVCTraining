using System.Dynamic;
using ASPNETCoreMVCTraining.Interfaces;
using ASPNETCoreMVCTraining.ViewModels;

namespace ASPNETCoreMVCTraining.Services;

public class PageService : IPageService
{
    public ExpandoObject GetPageParams(int pageIndex, PageInfo pageInfo)
    {
        var paramData= new ExpandoObject();
        paramData.TryAdd("pageIndex", pageIndex);
        paramData.TryAdd("pageSize", pageInfo.PageSize);

        foreach (var item in pageInfo.Params)
        {
            paramData.TryAdd(item.Key, item.Value);
        }

        return paramData;
    }
}