﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataAccess.Entities
{
    [Table( nameof( Coin ) )]
    public class Coin : BaseEntity
    {
        [Required] [MaxLength( 256 )] public string Name { get; set; }

        [Required] [MaxLength( 128 )] public string Identifier { get; set; }

        public List<Order>? Orders { get; set; }

        public Coin( string name, string identifier, List<Order>? orders )
        {
            Name       = name;
            Identifier = identifier;
            Orders     = orders;
        }
    }
}