﻿namespace SeatGeek.Web.ViewModels.Event
{
    using SeatGeek.Data.Models.Enums;
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Ticket;
    public class TicketFormModel
    {
        [Required(ErrorMessage = "Event ID is required")]
        public int EventId { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }


        [EnumDataType(typeof(TicketTypeEnum), ErrorMessage = "Type is unvalid")]
        public string Type { get; set; } = null!;

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1,MaxQuantity, ErrorMessage = "Quantity must be between 1 and {1}")]
        public int Quantity { get; set; }


    }
}