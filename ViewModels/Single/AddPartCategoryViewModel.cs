using ComputerRepairService.Models.Dtos;
using ComputerRepairService.Models.Servicess;
using ComputerRepairService.ViewModels.Single;
using ComputerRepairService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Diagnostics.Metrics;
using System.IO;
using ComputerRepairService.Helpers;

namespace ComputerRepairService.ViewModels
{
    public class AddPartCategoryViewModel : BaseCreateViewModel<PartCategoryService, PartCategoryDto, PartCategory>
    {
        public string CategoryName
        {
            get => Model.CategoryName;
            set
            {
                if (Model.CategoryName != value)
                {
                    Model.CategoryName = value;
                    OnPropertyChanged(() => CategoryName);
                }
            }
        }
        public string? CategoryDescription
        {
            get => Model.CategoryDescription;
            set
            {
                if (Model.CategoryDescription != value)
                {
                    Model.CategoryDescription = value;
                    OnPropertyChanged(() => CategoryDescription);
                }
            }
        }
        private int _numberOfActiveCategories;
        public int NumberOfActiveCategories
        {
            get => _numberOfActiveCategories;
            set
            {
                if (_numberOfActiveCategories != value)
                {
                    _numberOfActiveCategories = value;
                    OnPropertyChanged(() => NumberOfActiveCategories);
                }
            }
        }

        public AddPartCategoryViewModel() : base("New Category")
        {
            ClearInputsCommand = new BaseCommand(() => ClearInputFields());
            NumberOfActiveCategories = Service.InitializeNumberOfActivePartCategories();
        } public AddPartCategoryViewModel(int id) : base(id,"Category")
        {
            ClearInputsCommand = new BaseCommand(() => ClearInputFields());
            NumberOfActiveCategories = Service.InitializeNumberOfActivePartCategories();
        }
        public override void ClearInputFields()
        {
            CategoryDescription = String.Empty;
            CategoryName = String.Empty;
        }
    }
}
