using Microsoft.EntityFrameworkCore;
using PointSystem.Data;
using PointSystem.Model.Entity;
using PointSystem.Model.Enum;
using System;

namespace PointSystem.Repository
{
    public class ControleDePontoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Ponto> _dbSet;

        public ControleDePontoRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Ponto>();
        }

        internal List<Ponto> GetAllPontos(string idUser)
        {
            return _dbSet.Where(x => x.IdUser == idUser).ToList();
        }

        internal void Add(Ponto entity)
        {
              _dbSet.Add(entity);
              _context.SaveChanges();
        }

        internal Ponto? Get(string idUser, TypePonto tipoPonto, DateOnly data)
        {
            return _dbSet.Where(x => x.IdUser == idUser && x.TipoDePonto == tipoPonto && x.Data == data).FirstOrDefault();
        }
    }
}
