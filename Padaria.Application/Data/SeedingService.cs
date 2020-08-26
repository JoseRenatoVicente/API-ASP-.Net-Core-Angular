using Microsoft.EntityFrameworkCore.Internal;
using Padaria.Domain.Entities;
using Padaria.Infra.Data.Context;
using System.Linq;

namespace Padaria.Application.Data
{
    public class SeedingService
    {
        private DataContext _context;

        public SeedingService(DataContext context)
        {
            _context = context;
        }


        public void Seed()
        {
            if(_context.Produtos.Any())
            {
                return;
            }


            Produto p1 = new Produto("Nutella", 20.50, "Muito boa", "1", "img.jpg");
            Produto p2 = new Produto("Banana", 3, "Muito boa", "1", "img.jpg");
            Produto p3 = new Produto("Maça", 2, "Muito boa", "0", "img.jpg");
            Produto p4 = new Produto("Pera", 1.50, "Muito boa", "1", "img.jpg");
            Produto p5 = new Produto("Barra de Chocolate", 6.90, "Muito boa", "0", "img.jpg");
            Produto p6 = new Produto("Bala", 0.50, "Muito boa", "1", "img.jpg");
            Produto p7 = new Produto("Salgadinho", 5, "Muito boa", "0", "img.jpg");
            Produto p8 = new Produto("Chiclete", 0.70, "Muito boa", "1", "img.jpg");
            Produto p9 = new Produto("Bolo", 20, "Muito boa", "0", "img.jpg");
            Produto p10 = new Produto("Pão", 10, "Muito boa", "1", "img.jpg");


            _context.Produtos.AddRange(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
             

            /*for (int i = 0; i < 20000; i++)
            {
                Produto p1 = new Produto(Guid.NewGuid(), "Nutella", 20.50, "Muito boa", "1", "img.jpg");
                _context.Produtos.AddRange(p1);
            }*/
            

            _context.SaveChanges();
            
        }
    }
}