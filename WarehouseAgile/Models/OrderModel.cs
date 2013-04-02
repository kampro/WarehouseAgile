using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WarehouseAgile.Models
{
    public class OrderModel
    {
        #region Properties

        [HiddenInput(DisplayValue = false)]
        public int Id { get; private set; }

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

        [DisplayName("Rodzaj wyposażenia")]
        public string EquipmentType { get; set; }

        [DisplayFormat(DataFormatString = "{0} zł")]
        [DisplayName("Cena wyposażenia")]
        public float EquipmentPrice { get; set; }

        [DisplayName("Status zamówienia")]
        public string Status { get; set; }

        [DisplayName("Klient")]
        public string Customer { get; set; }

        [DisplayName("Sprzedawca")]
        public string Seller { get; set; }

        #endregion

        #region Public Methods

        public static OrderModel CreateFromEntity(Order orderEntity)
        {
            if (orderEntity == null)
                return null;

            return new OrderModel
            {
                Branch = orderEntity.Seller.Branch.Name,
                CarMake = orderEntity.EquipmentPrice.Model.Make.Name,
                CarModel = orderEntity.EquipmentPrice.Model.Name,
                Color = orderEntity.Color.Name,
                OrderDate = orderEntity.Date,
                ModelPrice = orderEntity.EquipmentPrice.Model.Price,
                EquipmentType = orderEntity.EquipmentPrice.Equipment.Name,
                EquipmentPrice = orderEntity.EquipmentPrice.Price,
                Status = orderEntity.State.Name,
                Id = orderEntity.Id,
                Customer = string.Format("{0} {1}", orderEntity.Customer.Name, orderEntity.Customer.Surname),
                Seller = string.Format("{0} {1}", orderEntity.Seller.Name, orderEntity.Seller.Surname)
            };
        }

        #endregion
    }
}