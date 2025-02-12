using CommunityToolkit.Mvvm.Messaging;
using ComputerRepairService.Helpers;
using ComputerRepairService.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerRepairService.ViewModels.Many
{
    public class CustomersWithCallbackViewModel : CustomersViewModel
    {
        public object WhoRequestedToSelect { get; set; } = default!;
        public CustomersWithCallbackViewModel(object whoRequestedToSelect)
        {
            WhoRequestedToSelect = whoRequestedToSelect;
        }
        protected override void HandleSelect()
        {
            WeakReferenceMessenger.Default.Send<SelectedObjectMessage<CustomerDto>>(new SelectedObjectMessage<CustomerDto>(WhoRequestedToSelect,SelectedModel!));
            OnRequestClose();
        }
    }
}
