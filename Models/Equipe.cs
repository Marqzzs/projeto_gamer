using System.ComponentModel.DataAnnotations;

namespace projeto_gamer.Models
{
    public class Equipe
    {
        [Key] //data annotation - IdEquipe
        public int IdEquipe { get; set; }
        public string? Nome { get; set; }        
        public string? Imagem { get; set; }

        //referencia que a classe equipe vai ter acesso a collectionm "Jogador"
        public ICollection<Jogador> Jogador {get; set;}
    }
}