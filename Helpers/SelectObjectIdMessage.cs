using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerRepairService.Helpers
{
    public class SelectObjectIdMessage
    {
        public object WhoRequestedToSelect {  get; set; }
        public int SelectedObjectId { get; set; }
        public SelectObjectIdMessage(object whoRequestedToSelect, int selectedObjectId)
        {
            WhoRequestedToSelect = whoRequestedToSelect;
            SelectedObjectId = selectedObjectId;
        }
    }
}
