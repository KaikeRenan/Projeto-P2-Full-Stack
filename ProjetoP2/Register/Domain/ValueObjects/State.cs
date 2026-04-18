namespace ProjetoP2.Register.Domain.ValueObjects
{
    public class State
    {
        public string Value { get; set; }

        public State(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length < 2)
            {
                throw new ArgumentException("Estado com formato inválido");
            }

            this.Value = value.Trim();
        }
    }
}
