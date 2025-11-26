namespace GestaoGastos.Application.Services.Interfaces
{
    public interface IAutenticacaoService
    {
        string GerarJwtToken(string email, string nomeUsuario, string nivel, int usuarioId);
        string CriptografarSenha(string senha);
    }
}
