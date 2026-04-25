using ProjetoP2.Register.Domain.ValueObjects;
using System.Text.RegularExpressions;

namespace ProjetoP2.Clinic.Domain.ValueObjects
{
    public class CRMV
    {
        public string Value { get; private set; }

        public CRMV(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("CRMV é obrigatório");
            }

            value = value.Trim().ToUpper();

            // Formato esperado: 12345-SP
            var parts = value.Split('-');

            if (parts.Length != 2)
            {
                throw new ArgumentException("CRMV deve estar no formato, exemplo: 12345-UF");
            }

            var number = parts[0];
            var state = parts[1];

            if (!number.All(char.IsDigit) || number.Length < 4 || number.Length > 6)
            {
                throw new ArgumentException("Número do CRMV inválido");
            }

            if (state.Length != 2 || !state.All(char.IsLetter))
            {
                throw new ArgumentException("UF do CRMV inválida");
            }

            Value = value;
        }
    }
}
