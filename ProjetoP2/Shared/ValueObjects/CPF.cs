namespace ProjetoP2.Shared.ValueObjects
{
    public class CPF
    {
        public string Value { get; set; } 

        public CPF(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length < 11) 
            { 
                throw new ArgumentException("CPF com formato inválido"); 
            }

            this.Value = value.Trim();
        }
    }
}
