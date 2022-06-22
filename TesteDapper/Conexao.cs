using Npgsql;
using Dapper;

namespace TesteDapper
{
    public class Conexao
    {
        private string stringconn = "Host=localhost;Port=5432;Database=TesteDynamic;Username=postgres;Password=adm";
        NpgsqlConnection conn;

        public Conexao()
        {
            conn = new NpgsqlConnection(stringconn);
        }

        public List<Carro> Obter()
        {
            string sql = $@"SELECT 
                        c.modelo as Modelo,
                        c.ano as Ano, 
                        c.carroid as CarroId,
                        f.nome as Nome
                        FROM carros c
                        LEFT JOIN fabricante f on f.fabricanteid = c.fabricanteid";

            conn.Open();
            var resultadoQuery = conn.Query<dynamic>(sql).ToList();

            object modeloRetorno = null;
            object anoRetorno = null;
            object carroidRetorno = null;
            object nomeRetorno = null;
            var listaRetorno = new List<Carro>();
            foreach (var row in resultadoQuery)
            {
                var fields = row as IDictionary<string, object>;
                fields.TryGetValue("modelo", out modeloRetorno);
                fields.TryGetValue("ano", out anoRetorno);
                fields.TryGetValue("carroid", out carroidRetorno);
                fields.TryGetValue("nome", out nomeRetorno);

                var objects = new object[] { modeloRetorno, anoRetorno, carroidRetorno, nomeRetorno };
                var modelo = objects[0] as string;
                var ano = objects[1] as int?;
                var carroid = objects[2] as int?;
                var nome = objects[3] as string;

                listaRetorno.Add(
                    new Carro
                    {
                        Modelo = modelo,
                        Ano = ano,
                        CarroId = carroid,
                        Fabricante = new Fabricante
                        {
                            Nome = nome
                        }
                    });
            }
            conn.Close();
            return listaRetorno;
        }

    }
}
