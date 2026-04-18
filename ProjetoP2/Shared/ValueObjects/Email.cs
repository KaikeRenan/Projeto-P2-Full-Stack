namespace ProjetoP2.Shared.ValueObjects
{
    public class Email
    {
        public string Value { get; set; }

        public Email(string value)
        {
            if (string.IsNullOrEmpty(value) || !value.Contains("@"))
            {
                throw new ArgumentException("Email com formato inválido");
            }

            this.Value = value.Trim();
        }
    }
}
