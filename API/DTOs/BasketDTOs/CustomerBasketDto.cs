using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Entities.Basket;

namespace API.DTOs.BasketDTOs
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItem> Items { get; set; }
    }
}