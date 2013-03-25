using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WarehouseAgile.Models
{
    public class OrderViewModel
    {
        #region Properties

        public int Id { get; private set; }

        public int StatusId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MM yyyy}")]
        [DisplayName("Data zamówienia")]
        public DateTime OrderDate { get; set; }

        [DisplayName("Samochód")]
        public string CarFullName
        {
            get
            {
                return string.Format("{0} {1}", CarMake, CarModel);
            }
        }

        [DisplayName("Model")]
        public string CarModel { get; set; }

        [DisplayName("Marka")]
        public string CarMake { get; set; }

        [DisplayName("Salon")]
        public string Branch { get; set; }

        [DisplayName("Kolor")]
        public string Color { get; set; }

        [DisplayFormat(DataFormatString = "{0} zł")]
        [DisplayName("Całkowita cena")]
        public float TotalPrice
        {
            get
            {
                return ModelPrice + EquipmentPrice;
            }
        }

        [DisplayFormat(DataFormatString = "{0} zł")]
        [DisplayName("Cena modelu")]
        public float ModelPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0} zł")]
        [DisplayName("Cena wyposażenia")]
        public float EquipmentPrice { get; set; }

        [DisplayName("Status zamówienia")]
        public string Status { get; set; }

        #endregion

        #region Public Methods

        public static OrderViewModel CreateFromEntity(Order orderEntity)
        {
            if (orderEntity == null)
                return null;
            
            return new OrderViewModel
            {
                Branch = orderEntity.Seller.Branch.Name,
                CarMake = orderEntity.EquipmentPrice.Model.Make.Name,
                CarModel = orderEntity.EquipmentPrice.Model.Name,
                Color = orderEntity.Color.Name,
                OrderDate = orderEntity.Date,
                ModelPrice = orderEntity.EquipmentPrice.Model.Price,
                EquipmentPrice = orderEntity.EquipmentPrice.Price,
                Status = orderEntity.State.Name,
                Id = orderEntity.Id,
                StatusId = orderEntity.Id_state
            };
        }

        #endregion
    }
}