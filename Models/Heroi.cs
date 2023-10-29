using System;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Heroi
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string NomeHeroi { get; set; }
        
        [Required]
        public DateTime DataNascimento { get; set; }

        [Required]
        public float Altura { get; set; }

        [Required]
        public float Peso { get; set; }

        [Required]
        public string SuperPoder { get; set; }
    }
}