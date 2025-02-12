using ComputerRepairService.Helpers;
using ComputerRepairService.Models.Servicess;
using ComputerRepairService.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ComputerRepairService.Models.Dtos;

namespace ComputerRepairService.ViewModels.Many
{
        abstract public class BaseManyViewModel<ServiceType, DtoType, ModelType>
        : BaseServiceViewModel<ServiceType, DtoType, ModelType>
        where ServiceType : BaseService<DtoType, ModelType>, new()
        where DtoType : class
        where ModelType : new()
        {
            
            private ObservableCollection<DtoType> _Models;
            public ObservableCollection<DtoType> Models
            {
                get => _Models;
                set
                {
                    if (_Models != value)
                    {
                        _Models = value;
                        OnPropertyChanged(() => Models);
                    }
                }
            }

            public ICommand EditCommand { get; set; }
            public ICommand RefreshCommand { get; set; }
            public ICommand DeleteCommand { get; set; }
            public ICommand CreateNewCommand { get; set; }
            public ICommand FilterCommand { get; set; }
            public ICommand ClearFiltersCommand { get; set; }
            public string? SearchInput
            {
                get => Service.SearchInput;
                set
                {
                    if (Service.SearchInput != value)
                    {
                        Service.SearchInput = value;
                        OnPropertyChanged(() => SearchInput);
                    }
                }
            }
            public List<YesComboBoxItemDto> YesNoOptions { get; set; }
            public List<SearchComboBoxDto> SearchComboBoxDto { get; set; }
            public List<SearchComboBoxDto> OrderComboBoxDto { get; set; }
            
            private DtoType? _SelectedModel;
            public DtoType? SelectedModel
            {
                get => _SelectedModel;
                set
                {
                    if (_SelectedModel != value)
                    {                   
                        _SelectedModel = value;
                        OnPropertyChanged(() => SelectedModel);
                        //if (SelectedModel != null)
                        //{
                        //    HandleSelect();
                        //}
                }
                }
            }
        public string? SearchProperty
        {
            get => Service.SearchProperty;
            set
            {
                if (Service.SearchProperty != value)
                {
                    Service.SearchProperty = value;
                    OnPropertyChanged(() => SearchProperty);
                }
            }
        }
        public bool OrderAscending
        {
            get => Service.OrderAscending;
            set
            {
               if(Service.OrderAscending != value)
               {
                    Service.OrderAscending = value;
                    OnPropertyChanged(() => OrderAscending);
               }
            }
        }
        public string? OrderProperty
        {
            get => Service.OrderProperty;
            set
            {
                if (Service.OrderProperty != value)
                {
                    Service.OrderProperty = value;
                    OnPropertyChanged(() => OrderProperty);
                }
            }
        }
        public BaseManyViewModel(string displayName) : base(displayName)
        {
            _Models = default!;
            Refresh();
            RefreshCommand = new BaseCommand(() => Refresh());
            DeleteCommand = new BaseCommand(() => Delete());
            CreateNewCommand = new BaseCommand(() => CreateNew());
            FilterCommand = new BaseCommand(() => Refresh());
            EditCommand = new BaseCommand(() => HandleSelect());
            ClearFiltersCommand = new BaseCommand(() => ClearFilters());
            YesNoOptions = new()
            {
                new YesComboBoxItemDto()
                {
                    SelectedOption = YesNoEnum.Yes,
                    OptionName = "Yes"
                },
                new YesComboBoxItemDto()
                {
                    SelectedOption = YesNoEnum.No,
                    OptionName = "No"
                },
                new YesComboBoxItemDto()
                {
                    SelectedOption = YesNoEnum.NoFilter,
                    OptionName = "No Filter"
                }
            };
            OrderComboBoxDto = Service.GetOrderByComboBoxDtos();
            SearchComboBoxDto = Service.GetSearchComboBoxDtos();
            SearchProperty = SearchComboBoxDto.FirstOrDefault()?.PropertyTitle;
            OrderProperty = OrderComboBoxDto.FirstOrDefault()?.PropertyTitle;
            }
            protected void Refresh()
            {
                Models = new ObservableCollection<DtoType>(Service.GetModels());
            }
            private void Delete()
            {
                if (SelectedModel != null)
                {
                    Service.DeleteModel(SelectedModel);
                    Models.Remove(SelectedModel);
                }
            }
            
            protected abstract void ClearFilters();
            //abstract method to be implemented in vm's
            //allows for creating new window
            protected abstract void CreateNew();
            protected virtual void HandleSelect() { }
        }
    }
