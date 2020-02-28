using System.Collections.Generic;

namespace WebExchangeApi.Models
{
    public class DistributionListModel
    {
        public string Email { get; set; }
        public IList<DistributionListModel> ChildEmailList { get; set; }
    }
}