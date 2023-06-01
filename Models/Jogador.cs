using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto_gamer.Models
{
    public class Jogador
    {
        [Key]
        public int IdJogador { get; set; }

        [Required]
        public string? Nome { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Senha { get; set; }

        [ForeignKey("Equipe")] //data annotation importando a key estrangeira da classe equipe
        public int IdEquipe { get; set; }
        public Equipe? Equipe {get; set;}
    }
}