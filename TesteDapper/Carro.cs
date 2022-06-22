namespace TesteDapper
{
    public class Carro
    {
        public int? CarroId { get; set; }
        public Fabricante Fabricante { get; set; }
        public string Modelo { get; set; }
        public int? Ano { get; set; }
    }
}
