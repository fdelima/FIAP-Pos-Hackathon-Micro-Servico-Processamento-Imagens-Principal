using System.Net.Http.Headers;
using System.Reflection;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain
{
    public static class Util
    {
        public static Type[] GetTypesInNamespace(string nameSpace)
        {
            return Assembly.GetExecutingAssembly()
                            .GetTypes()
                            .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                            .ToArray();
        }

        public static HttpClient GetClient(string baseAddress)
        {
            var _client = new HttpClient();
            _client.BaseAddress = new Uri(baseAddress);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return _client;
        }
    }
}
