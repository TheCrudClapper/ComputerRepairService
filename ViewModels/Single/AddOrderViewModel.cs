using ComputerRepairService.Models;
using ComputerRepairService.Models.Dtos;
using ComputerRepairService.Models.Servicess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerRepairService.ViewModels.Single
{
    public class AddOrderViewModel : BaseCreateViewModel<PartOrderService, PartOrderDto, PartOrder>
    {
        private int _NumberOfActiveDeliveries { get; set; }
        public int NumberOfActiveDeliveries
        {
            get => _NumberOfActiveDeliveries;
            set
            {
                if(_NumberOfActiveDeliveries != value)
                {
                    _NumberOfActiveDeliveries = value;
                    OnPropertyChanged(() => NumberOfActiveDeliveries);
                }
            }
        }
        public int SupplierId
        {
            get => Model.SupplierId;
            set
            {
                if (Model.SupplierId != value)
                {
                    Model.SupplierId = value;
                    OnPropertyChanged(() => SupplierId);
                }
            }
        }
        public int PartId
        {
            get => Model.PartId;
            set
            {
                if(Model.PartId != value)
                {
                    Model.PartId = value;
                    OnPropertyChanged(() => PartId);
                }
            }
        }
        public int QuantityOrdered
        {
            get => Model.QuantityOrdered;
            set
            {
                if (Model.QuantityOrdered != value)
                {
                    Model.QuantityOrdered = value;
                    OnPropertyChanged(() => QuantityOrdered);
                    if (PartId != default)
                    {
                        TotalWorth = Service.GetTotalOrderWorthByPartId(PartId, value);
                    }
                   
                }
            }
        }
        public DateTime OrderDate
        {
            get => Model.OrderDate;
            set
            {
                if(Model.OrderDate != value)
                {
                    Model.OrderDate = value;
                    OnPropertyChanged(() => OrderDate);
                }
            }
        }
        public DateOnly? DeliveryDate
        {
            get => Model.DeliveryDate;
            set
            {
                if (Model.DeliveryDate != value)
                {
                    Model.DeliveryDate = value;
                    OnPropertyChanged(() => DeliveryDate);
                }
            }
        }
        private decimal _TotalWorth {  get; set; }
        public decimal TotalWorth
        {
            get => _TotalWorth;
            set
            {
                if(_TotalWorth != value)
                {
                    _TotalWorth = value;
                    OnPropertyChanged(() => TotalWorth);
                }
            }
        }
        private ObservableCollection<ComboBoxDto> _Parts {  get; set; }
        public ObservableCollection<ComboBoxDto> Parts
        {
            get => _Parts;
            set
            {
                if (_Parts != value)
                {
                    _Parts = value;
                    OnPropertyChanged(() => Parts);
                }
            }
        }
        private ObservableCollection<ComboBoxDto> _Suppliers { get; set; }
        public ObservableCollection<ComboBoxDto> Suppliers
        {
            get => _Suppliers;
            set
            {
                if (_Suppliers != value)
                {
                    _Suppliers = value;
                    OnPropertyChanged(() => Suppliers);
                }
            }
        }
        public AddOrderViewModel() : base("Order")
        {
            NumberOfActiveDeliveries = Service.InitializeNumberOfActiveOrder();
            Suppliers = Service.InitializeSuppliersComboBox();
            OrderDate = DateTime.Now;
            Parts = Service.InitializePartsComboBox();
        }
        public AddOrderViewModel(int id) : base(id, "Order")
        {
            NumberOfActiveDeliveries = Service.InitializeNumberOfActiveOrder();
            Suppliers = Service.InitializeSuppliersComboBox();
            OrderDate = DateTime.Now;
            Parts = Service.InitializePartsComboBox();
        }
        public override void ClearInputFields()
        {
            OrderDate = DateTime.Now;
            QuantityOrdered = 0;
            PartId = default;
            SupplierId = default;
            DeliveryDate = default;
        }
    }
}
