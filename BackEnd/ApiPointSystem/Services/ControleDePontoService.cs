using PointSystem.Data;
using PointSystem.DTOs;
using PointSystem.Model.Entity;
using PointSystem.Model.Enum;
using PointSystem.Repository;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PointSystem.Services
{
    public class ControleDePontoService
    {
        private ControleDePontoRepository _repository;

        public ControleDePontoService(ApplicationDbContext context)
        {
            this._repository = new ControleDePontoRepository(context);
        }

        public List<GetPontosDTOs> Get(string idUser)
        {
            var result = new List<GetPontosDTOs>();
            var pontos = _repository.GetAllPontos(idUser);
            var registroPorData = pontos.OrderByDescending(x => x.Data).Select(x => x.Data).GroupBy(x => x.ToString("yyyy-MM-dd"));

            foreach (var grupo in registroPorData.ToList())
            {
                var data = DateOnly.Parse(grupo.Key);
                var entrada = pontos.Where(x => x.Data == data && x.TipoDePonto == TypePonto.Entrata).FirstOrDefault()?.Hora;
                var saida = pontos.Where(x => x.Data == data && x.TipoDePonto == TypePonto.Saida).FirstOrDefault()?.Hora;
                result.Add(new GetPontosDTOs
                {
                    Data = data.ToString("dd/MM/yyyy"),
                    Entrada = entrada?.ToString("HH:mm"),
                    Saida = saida?.ToString("HH:mm")
                });
            }

            return result;
        }

        public PostPontoDTO Add(string idUser, TypePonto tipoPonto)
        {
            var ponto = new Ponto
            {
                Data = DateOnly.FromDateTime(DateTime.Now),
                Hora = TimeOnly.FromDateTime(DateTime.Now),
                IdUser = idUser,
                TipoDePonto = tipoPonto
            };

            //verificar se existe registro!
            Ponto? existePonto = _repository.Get(idUser, tipoPonto, ponto.Data);
            if (existePonto != null)
                throw new Exception($@"Ponto de {tipoPonto.ToString()} já registrado para a data {ponto.Data} ");

            //verificar se existe registro de entrada!
            if (tipoPonto.Equals(TypePonto.Saida))
            {
                Ponto? existePontoEntrada = _repository.Get(idUser, TypePonto.Entrata, ponto.Data);
                if (existePontoEntrada == null)
                    throw new Exception($@"Ponto de {TypePonto.Entrata}  não foi registrado para a data {ponto.Data} ");
            }

            _repository.Add(ponto);

            return new PostPontoDTO
            {
                DataDoRegistro = ponto.Data.ToString("dd/MM/yyyy"),
                HorasDoRegistro = ponto.Hora.ToString("HH:mm"),
                TipoDoRegistro = ponto.TipoDePonto.ToString()
            };
        }
    }
}
