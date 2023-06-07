using Microsoft.AspNetCore.Mvc;
using projeto_gamer.Infra;
using projeto_gamer.Models;

namespace projeto_gamer.Controllers
{
    [Route("[controller]")]
    public class EquipeController : Controller
    {
        private readonly ILogger<EquipeController> _logger;

        public EquipeController(ILogger<EquipeController> logger)
        {
            _logger = logger;
        }

        //instancia do contexto para acessar o banco de dados
        Context c = new Context();

        [Route("Listar")] //http://localhost/Equipe/Listar
        public IActionResult Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            //variavel que armazena as equipes listadas no banco
            ViewBag.Equipe = c.Equipe.ToList();

            //retorna a view na tela
            return View();
        }

        [Route("Cadastrar")]//Da rota cadastrar
        public IActionResult Cadastrar(IFormCollection form) //iformcolection é uma classe de formulario
        {
            //Istancia um objeto da model Equipe
            Equipe novaEquipe = new Equipe();

            //Pelo objeto novaEquipe, chamo a função de forms e faço com que entre os dados
            novaEquipe.Nome = form["Nome"].ToString();

            //aqui estaca chegando como string
            // novaEquipe.Imagem = form["Imagem"].ToString();

            //inicio da logica do upload da imagem
            if (form.Files.Count > 0)
            {
                var file = form.Files[0];

                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                //gera o caminho completo at[e o caminho do arquivo(imagem - nome da extensao)
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", folder, file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                novaEquipe.Imagem = file.FileName;
            }
            else
            {
                novaEquipe.Imagem = "padrao.png";
            }


            //Pelo context adiciona o objeto novaequipe ao banco de dados com os dados inseridos
            c.Equipe.Add(novaEquipe);

            //salva as alterações no banco de dados
            c.SaveChanges();

            //retorna para o local chamada a rota de listar(metodo index)
            return LocalRedirect("~/Equipe/Listar");
        }

        [Route("Excluir/{id}")]
        public IActionResult Excluir(int id)
        {
            //expressão lambida para encontrar a equipe que deseja excluir pelo id
            Equipe equipeEncontrada = c.Equipe.FirstOrDefault(e => e.IdEquipe == id);

            //remove do banco de dados
            c.Remove(equipeEncontrada);

            //salva as alterações
            c.SaveChanges();

            //retorna para o local chamada a rota de listar(metodo index)
            return LocalRedirect("~/Equipe/Listar");
        }

        [Route("Edit/{id}")]
        public IActionResult Editar(int id)
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            
            Equipe equipe = c.Equipe.FirstOrDefault(x => x.IdEquipe == id);

            ViewBag.Equipe = equipe;

            return View("Edit");
        }

        [Route("Atualizar")]
        public IActionResult Atualizar(IFormCollection form)
        {
            Equipe novaEquipe = new Equipe();

            novaEquipe.IdEquipe = int.Parse(form["IdEquipe"].ToString());

            novaEquipe.Nome = form["Nome"].ToString();

            if (form.Files.Count > 0)
            {
                var file = form.Files[0];

                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                //gera o caminho completo at[e o caminho do arquivo(imagem - nome da extensao)
                var path = Path.Combine(folder, file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                novaEquipe.Imagem = file.FileName;
            }
            else
            {
                novaEquipe.Imagem = "padrao.png";
            }

            Equipe equipeBuscada = c.Equipe.First(x => x.IdEquipe == novaEquipe.IdEquipe);

            equipeBuscada.Nome = novaEquipe.Nome;
            equipeBuscada.Imagem = novaEquipe.Imagem;

            c.Equipe.Update(equipeBuscada);

            c.SaveChanges();

            return LocalRedirect("  Equipe/Listar");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}