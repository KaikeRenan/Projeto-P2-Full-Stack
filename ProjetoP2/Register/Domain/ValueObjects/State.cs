namespace ProjetoP2.Register.Domain.ValueObjects
{
    public class State
    {
        public string Value { get; private set; }

        public State(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length < 2)
            {
                throw new ArgumentException("Sigla de estado com formato inválido");
            }

            this.Value = value.Trim();
        }
    }
}
