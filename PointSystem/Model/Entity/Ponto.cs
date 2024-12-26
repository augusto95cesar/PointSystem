using PointSystem.Model.Enum;

namespace PointSystem.Model.Entity
{
    public class Ponto
    {
        public int Id { get; set; }
        public string IdUser { get; set; } = "";
        public DateTime DataRegistro { get; set; }
        public TypePonto TipoDePonto { get; set; }
    }
}
