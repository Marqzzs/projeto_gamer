using Microsoft.EntityFrameworkCore;
using projeto_gamer.Models;

namespace projeto_gamer.Infra
{
    public class Context : DbContext
    {
        public Context()
        {           
        }

        public Context(DbContextOptions<Context> options) : base(options)
        { 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //string de conexão com o banco
                //Data source : o nome do servidor do gerenciador do banco
                //initial catalog : vai ser o nome do banco de dados

                //autentificação pelo windows
                //Integrated Security : Autenticação pell windows
                //Integrated Certificate : Autenticação pell windows

                //autentificação pelo SQLServer
                //USer ID = "nome do seu usuario de login"
                //password = senha do seu usuario
                optionsBuilder.UseSqlServer(@"Data Source = NOTE19-S15; initial catalog = gamerManha; User Id = sa; pwd = Senai@134; TrustServerCertificate = true");
            }
        }
        //referencia de classes e tabelas
        public DbSet<Jogador> Jogador{get; set;}
        public DbSet<Equipe> Equipe{get; set;}
    }
}