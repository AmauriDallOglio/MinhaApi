using Microsoft.EntityFrameworkCore;
using MinhaApi.Entidade;

namespace MinhaApi.Contexto
{
    public class AgendaContexto : DbContext
    {
        public AgendaContexto(DbContextOptions<AgendaContexto> options) : base(options)
        { 
        }

        public DbSet<Contato> contatos { get; set; }

    }
}
