using Padaria.Repository;
using Padaria.Domain;

using Microsoft.EntityFrameworkCore.Internal;

namespace Padaria.WebAPI.Data
{
    public class SeedingService
    {
        private DataContext _context;

        public SeedingService(DataContext context)
        {
            _context = context;
        }


        public void Seed()
        {/*
            if(_context.Produtos.Any())
            {

                return;
            }

            Produto p1=new Produto(1, "Nutela", 10, "efef");
            Produto p2=new Produto(2, "Maça", 15, "efefdcsdc", "img.jpg");

            _context.Produtos.AddRange(p2);
      
            _context.SaveChanges();
            */
        }
    }
}