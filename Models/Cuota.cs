﻿using System.ComponentModel.DataAnnotations;

namespace ClubBlazor.Models
{
    public class Cuota
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;
        public decimal Monto { get; set; } // Nuevo campo para el monto

        // Relación con Deportistas (Uno a Muchos)
        public ICollection<Deportista>? Deportistas { get; set; }

        // Relación con Socios (Uno a Muchos)
        public ICollection<Socio>? Socios { get; set; }
    }
}
