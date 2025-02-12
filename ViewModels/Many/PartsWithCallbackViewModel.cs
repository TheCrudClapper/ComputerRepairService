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
    public class PartsWithCallbackViewModel : PartsViewModel
    {
        public object WhoRequestedToSelect { get; set; }
        public PartsWithCallbackViewModel(object whoRequestedToSelect)
        {
            WhoRequestedToSelect = whoRequestedToSelect;
        }
        protected override void HandleSelect()
        {
            WeakReferenceMessenger.Default.Send<SelectedObjectMessage<PartDto>>(new SelectedObjectMessage<PartDto>(WhoRequestedToSelect, SelectedModel!));
            OnRequestClose();
        }
    }
}
