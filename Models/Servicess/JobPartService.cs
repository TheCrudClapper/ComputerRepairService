using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ComputerRepairService.Models.Dtos;
using Microsoft.EntityFrameworkCore;
namespace ComputerRepairService.Models.Servicess
{
    public class JobPartService : BaseService<JobPartDto, JobPart>
    {
        public int availablePartQuantity { get; set; }
        public override void AddOrUpdateModel(JobPart model)
        {
            if (model.Id == default)
            {
                DatabaseContext.JobParts.Add(model);
                DatabaseContext.SaveChanges();
            }
            else
            {
                UpdateModel(model);
            }
        }

        public override void DeleteModel(JobPartDto model)
        {
            JobPart jobPart = DatabaseContext.JobParts.First(item => item.Id == model.Id);
            jobPart.IsActive = false;
            jobPart.DateDeleted = DateTime.Now;
            DatabaseContext.SaveChanges();
        }
        public override JobPart GetModel(int id)
        {
            return DatabaseContext.JobParts.First(item => item.Id == id);
        }

        public override List<JobPartDto> GetModels()
        {
            IQueryable<JobPartDto> JobPartDtos = DatabaseContext.JobParts
            .Where(item => item.IsActive)
            .Include(item => item.Part)
            .ThenInclude(item => item.PartCategory)
            .Select(item => new JobPartDto()
            {
                Id = item.Id,
                Cost = item.Cost,
                PartCategory = item.Part.PartCategory.CategoryName,
                PartName = item.Part.PartName,
                RepairJobId = item.JobId,
                QuantityUsed = item.QuantityUsed,
                TotalCost = item.Cost!.Value * item.QuantityUsed,
                DateCreated = item.DateCreated,
                DateEdited = item.DateEdited,
            });
            return JobPartDtos.ToList();
        }
        public override JobPart CreateModel()
        {
            return new JobPart()
            {
                IsActive = true,
                DateCreated = DateTime.Now,
            };
        }
        public override void UpdateModel(JobPart model)
        {
            DatabaseContext.JobParts.Update(model);
            DatabaseContext.SaveChanges();
        }
        public List<JobPartDto> GetModelsBySelectedRepairID(int RepairJobId)
        {
            return DatabaseContext.JobParts
            .Where(item => item.IsActive)
            .Where(item => item.JobId == RepairJobId)
            .Select(item => new JobPartDto()
            {
                Id = item.Id,
                Cost = item.Cost,
                PartCategory = item.Part.PartCategory.CategoryName,
                PartName = item.Part.PartName,
                RepairJobId = item.JobId,
                QuantityUsed = item.QuantityUsed,
                TotalCost = item.Cost!.Value * item.QuantityUsed,
                DateCreated = item.DateCreated,
                DateEdited = item.DateEdited,
            })
            .ToList();
        }
        public int InitializeNumberOfActivePart()
        {
            return DatabaseContext.Parts.Where(item => item.IsActive).Count();
        }
        //method that fills cost field in jobpart vm with default value of part unit price
        public decimal GetPartCostByPartId(int partId)
        {
            return DatabaseContext.Parts.Where(item => item.Id == partId).Select(item => item.UnitPrice).First();
        } 
        public int GetPartAvailableQuantity(int partId)
        {
            return DatabaseContext.Parts.Where(item => item.Id == partId).Select(item => item.QuantityInStock).First();
        }
        public void SubstractQuantityUsedFromDatabase(int partId, int quantityUsed)
        {
            var part = DatabaseContext.Parts.First(item => item.Id == partId);
            part.QuantityInStock -= quantityUsed;
            DatabaseContext.SaveChanges(); 
        }
        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            throw new NotImplementedException();
        }

        public override List<SearchComboBoxDto> GetOrderByComboBoxDtos()
        {
            throw new NotImplementedException();
        }
        public override string ValidateProperty(string columnName, JobPart model)
        {
            if (columnName == nameof(model.QuantityUsed))
            {
                if (model.QuantityUsed > availablePartQuantity)
                {
                    return "Too much parts used, correct your number";
                }
                else if (model.QuantityUsed == default)
                {
                    return "Part quatity can't be 0";
                }
                else if (model.QuantityUsed < 0)
                {
                    return "Part quantity can't be lower negative";
                }
            }
            return string.Empty;
        }
    }
}
