using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models;

[Table("orders")]
[Index("Ordernumber", Name = "orders_ordernumber_key", IsUnique = true)]
public partial class Order
{
    [Key]
    [Column("ordernumber")]
    [StringLength(20)]
    public string Ordernumber { get; set; } = null!;

    [Column("sendercity")]
    [StringLength(100)]
    public string Sendercity { get; set; } = null!;

    [Column("senderaddress")]
    [StringLength(255)]
    public string Senderaddress { get; set; } = null!;

    [Column("recipientcity")]
    [StringLength(100)]
    public string Recipientcity { get; set; } = null!;

    [Column("recipientaddress")]
    [StringLength(255)]
    public string Recipientaddress { get; set; } = null!;

    [Required(ErrorMessage = "Вес груза обязателен")]    
    [Column("cargoweight")]
    public decimal Cargoweight { get; set; }

    [Column("pickupdate")]
    public DateOnly Pickupdate { get; set; }

    [Column("createdat", TypeName = "timestamp without time zone")]
    public DateTime Createdat { get; set; }
}
