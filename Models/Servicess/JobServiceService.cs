using ComputerRepairService.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ComputerRepairService.Models.Servicess
{
    //this class implementation need to be ended
    public class JobServiceService : BaseService<JobServiceDto, JobService>
    {
        
        public override void AddOrUpdateModel(JobService model)
        {
            if(model.Id == default)
            {
                DatabaseContext.JobServices.Add(model);
                DatabaseContext.SaveChanges();
            }
            else
            {
                UpdateModel(model);
            }
        }

        public override void DeleteModel(JobServiceDto model)
        {
            JobService jobService = DatabaseContext.JobServices.First(item => item.Id == model.Id);
            jobService.IsActive = false;
            jobService.DateDeleted = DateTime.Now;
            DatabaseContext.SaveChanges();
        }

        public override JobService GetModel(int id)
        {
            return DatabaseContext.JobServices.First(item => item.Id == id);
        }

        public override List<JobServiceDto> GetModels()
        {
            IQueryable<JobServiceDto> JobServiceDto = DatabaseContext.JobServices.Where(item => item.IsActive) //Where(item => item.JobId == SelectedJobId)
            .Include(item => item.Service)
            .Select(item => new JobServiceDto()
            {
                Id = item.Id,
                DateCreated = item.DateCreated,
                DateEdited = item.DateEdited,
                RepairJobId = item.JobId,
                ServiceName = item.Service.ServiceName,
                Cost = item.Cost,
            });
            return JobServiceDto.ToList();
        }

        public override JobService CreateModel()
        {
            return new JobService()
            {
                IsActive = true,
                DateCreated = DateTime.Now,
            };
        }
        public override void UpdateModel(JobService model)
        {
            DatabaseContext.JobServices.Update(model);
            DatabaseContext.SaveChanges();
        }
        public List<JobServiceDto> GetModelsBySelectedRepairID(int RepairJobId)
        {
            return DatabaseContext.JobServices
            .Where(item => item.IsActive)
            .Where(item => item.JobId == RepairJobId)
            .Select(item => new JobServiceDto()
            {
                Id = item.Id,
                DateCreated = item.DateCreated,
                DateEdited = item.DateEdited,
                RepairJobId = item.JobId,
                ServiceName = item.Service.ServiceName,
                Cost = item.Cost,
            })
            .ToList();
        }
        public int InitializeNumberOfActiveServices()
        {
            return DatabaseContext.Services.Where(item => item.IsActive).Count();
        }
        //method that returns service cost based od service id
        public decimal GetServiceCostByServiceId(int serviceId)
        {
            return DatabaseContext.Services.Where(item => item.Id == serviceId).Select(item => item.BasePrice).First();
        }
        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            throw new NotImplementedException();
        }

        public override List<SearchComboBoxDto> GetOrderByComboBoxDtos()
        {
            throw new NotImplementedException();
        }
    }
}
