﻿using CommunityToolkit.Mvvm.Messaging;
using ComputerRepairService.Helpers;
using ComputerRepairService.Models.Dtos;

namespace ComputerRepairService.ViewModels.Many
{
    public class RepairsWithCallbackViewModel : RepairsViewModel
    {
        public object WhoRequestedToSelect { get; set; } = default!;

        public RepairsWithCallbackViewModel(object whoRequestedToSelect)
        {
            WhoRequestedToSelect = whoRequestedToSelect;
        }
        protected override void HandleSelect()
        {
            //send callback with selected repair job
            WeakReferenceMessenger.Default.Send<SelectedObjectMessage<RepairJobDto>>(new SelectedObjectMessage<RepairJobDto>(WhoRequestedToSelect, SelectedModel!));
            OnRequestClose();
        }
    }
}
