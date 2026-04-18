using System.Drawing;
using System.Net.NetworkInformation;

namespace ProjetoP2.Register.Domain.ValueObjects
{
    public class Color
    {
        private static readonly HashSet<string> OptionColors = new()
        {
            "Bicolor",
            "Branco",
            "Caramelo",
            "Cinza",
            "Escaminha",
            "Frajola",
            "Laranja",
            "Marrom",
            "Preto",
            "Tigrado",
            "Tricolor",
            "Outros"
        };

        public string Value { get; } 

        public Color (string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Cor inválida");

            if (!OptionColors.Contains(value))
                throw new ArgumentException("Cor não permitida");

            this.Value = value.Trim();
        }

        public override bool Equals(object obj) => obj is Color other && Value == other.Value;

        public override int GetHashCode() => Value.GetHashCode();
    }
}
