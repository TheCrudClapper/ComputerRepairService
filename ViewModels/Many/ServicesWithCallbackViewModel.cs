﻿using CommunityToolkit.Mvvm.Messaging;
using ComputerRepairService.Helpers;
using ComputerRepairService.Models.Dtos;

namespace ComputerRepairService.ViewModels.Many
{
    public class ServicesWithCallbackViewModel : ServicesViewModel
    {

        public object WhoRequestedToSelect { get; set; }
        public ServicesWithCallbackViewModel(object whoRequestedToSelect)
        {
            WhoRequestedToSelect = whoRequestedToSelect;
        }
        protected override void HandleSelect()
        {
            //Send the callback with selected service
            //Selected model is the one from data grid
            WeakReferenceMessenger.Default.Send<SelectedObjectMessage<ServiceDto>>(new SelectedObjectMessage<ServiceDto>(WhoRequestedToSelect, SelectedModel!));
            OnRequestClose();
        }
    }
}
