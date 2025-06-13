﻿using BusinessLogic.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Models
{
    [PrimaryKey("Id")]
    public class Order
    {
        [Key, Column("OrderId")]
        public long Id { get; set; }

        public string UserName { get; set; }

        public DateTime OrderDate { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public OrderStatus Status { get; set; }

        public float Rating { get; set; }
    }
}
