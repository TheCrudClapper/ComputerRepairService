using ComputerRepairService.Models.Contexts;
using ComputerRepairService.Models.Dtos;
using ComputerRepairService.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Windows.Input;
using ComputerRepairService.Helpers;
using ComputerRepairService.Models.Servicess;
using CommunityToolkit.Mvvm.Messaging;
using ComputerRepairService.ViewModels.Single;

namespace ComputerRepairService.ViewModels.Many
{
    public class PartsViewModel : BaseManyViewModel<PartService, PartDto, Part>
    {
        public decimal? PartPriceFrom
        {
            get => Service.PartPriceFrom;
            set
            {
                if (Service.PartPriceFrom != value)
                {
                    Service.PartPriceFrom = value;
                    OnPropertyChanged(() => PartPriceFrom);
                }
            }
        }
        public decimal? PartPriceTo
        {
            get => Service.PartPriceTo;
            set
            {
                if (Service.PartPriceTo != value)
                {
                    Service.PartPriceTo = value;
                    OnPropertyChanged(() => PartPriceTo);
                }
            }
        }
        public bool HasDescription
        {
            get => Service.HasDescription;
            set
            {
                if (Service.HasDescription != value)
                {
                    Service.HasDescription = value;
                    OnPropertyChanged(() => HasDescription);
                }
            }
        }
        public PartsViewModel() : base("Parts")
        {
            HasDescription = false;
            
        }
        protected override void ClearFilters()
        {
            HasDescription = false;
            PartPriceFrom = null;
            PartPriceTo = null;
            SearchInput = null;
            SearchProperty = SearchComboBoxDto.FirstOrDefault()?.PropertyTitle;
            OrderAscending = false;
            OrderProperty = OrderComboBoxDto.FirstOrDefault()?.PropertyTitle;
            Refresh();
        }
        protected override void HandleSelect()
        {
            if (SelectedModel != null)
            {
                WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
                {
                    Sender = this,
                    ViewModelToBeOpened = new AddPartViewModel(SelectedModel.Id)
                });

            }
        }
        protected override void CreateNew()
        {
            WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
            {
                Sender = this,
                ViewModelToBeOpened = new AddPartViewModel()
            });
        }
      
    }
}
