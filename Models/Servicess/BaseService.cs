using ComputerRepairService.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerRepairService.Models.Dtos;
namespace ComputerRepairService.Models.Servicess
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="DtoType">DTO do wyświetlenia na liście</typeparam>
    /// <typeparam name="ModelType">Model do dodania/edycji</typeparam>    
    public abstract class BaseService<DtoType, ModelType>
        where ModelType : new()
    {
        protected DatabaseContext DatabaseContext { get; set; }
        public string? SearchInput { get; set; }
        public string? SearchProperty { get; set; }
        public string? OrderProperty { get; set; }
        public bool OrderAscending { get; set; }
        public BaseService()
        {
            DatabaseContext = new DatabaseContext();
        }
        public abstract List<DtoType> GetModels();
        public abstract ModelType GetModel(int id);
        public abstract void AddOrUpdateModel(ModelType model);
        public abstract void UpdateModel(ModelType model);
        public abstract void DeleteModel(DtoType model);
        public abstract List<SearchComboBoxDto> GetSearchComboBoxDtos();
        public abstract List<SearchComboBoxDto> GetOrderByComboBoxDtos();
        public virtual ModelType CreateModel()
        {
            return new ModelType();
        } 
        /// </summary>
        /// <param name="columnName">Name of control that we are validating (name in binding)</param>
        /// <returns>Returns String.Empty if no errors or the error</returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual string ValidateProperty(string columnName, ModelType model)
        {
            return string.Empty;
        }
        public bool isValid(ModelType model)
        {
            return !typeof(ModelType).GetProperties().Any(item => ValidateProperty(item.Name,model) != string.Empty);
        }
    }
}

