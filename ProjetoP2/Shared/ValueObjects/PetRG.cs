namespace ProjetoP2.Shared.ValueObjects
{
    public class PetRG
    {
        public string Value { get; set; }

        public PetRG(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length < 15)
            {
                throw new ArgumentException("Registro Geral do Animal com formato inválido");
            }

            this.Value = value.Trim();
        }
    }
}
