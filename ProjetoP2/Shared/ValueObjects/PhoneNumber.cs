namespace ProjetoP2.Shared.ValueObjects
{
    public class PhoneNumber
    {
        public string Value { get; set; }

        public PhoneNumber(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length < 10)
            {
                throw new ArgumentException("Número de celular com formato inválido");
            }

            this.Value = value.Trim();
        }
    }
}
