using PointSystem.Model.Enum;

namespace PointSystem.Model.Entity
{
    public class Ponto
    {
        public int Id { get; set; }
        public string IdUser { get; set; } = "";
        public DateOnly Data { get; set; }
        public TimeOnly Hora { get; set; }
        public TypePonto TipoDePonto { get; set; }
    }
}
