using GestaoGastos.Application.InputModels;
using GestaoGastos.Application.Services.Interfaces;
using GestaoGastos.Domain.Entities;
using GestaoGastos.Domain.Enums;
using GestaoGastos.Domain.Interfaces;

using System.Linq.Expressions;

namespace GestaoGastos.Application.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        private IUsuarioRepository _usuarioRepository;
        private IAutenticacaoService _autenticacaoService;

        public UsuarioService(IUsuarioRepository usuarioRepository, IAutenticacaoService autenticacaoService)
        {
            _usuarioRepository = usuarioRepository;
            _autenticacaoService = autenticacaoService;
        }

        public async Task CadastrarUsuario(CadastroUsuarioInputModel inputModel)
        {
            var existeUsuarioMesmoEmail = await _usuarioRepository.ObterUsuariosQuery(x => x.Email == inputModel.Email);
            if (existeUsuarioMesmoEmail.Any())
            {
                throw new Exception("Já existe um usuário cadastrado com esse e-mail.");
            }

            var senhaHash = _autenticacaoService.CriptografarSenha(inputModel.Senha);
            var usuario = new Usuario(inputModel.Nome, inputModel.Email, senhaHash, ERole.Usuario);

            await _usuarioRepository.AdicionarUsuarioAsync(usuario);
        }

        public async Task<Usuario> GetUsuarioPorEmailESenha(string email, string senhaHash)
        {
            var usuario = await _usuarioRepository.ObterUsuariosQuery(x => x.Email == email && x.SenhaHash == senhaHash);

            if (!usuario.Any())
            {
                return null;
            }

            return usuario.First();
        }

        public async Task<IEnumerable<Usuario>> GetUsuariosQuery(Expression<Func<Usuario, bool>> query)
        {
            return await _usuarioRepository.ObterUsuariosQuery(query);
        }

        public async Task<string> LoginUsuario(LoginUsuarioInputModel inputModel)
        {
            var senhaHash = _autenticacaoService.CriptografarSenha(inputModel.Senha);
            var usuario = await GetUsuarioPorEmailESenha(inputModel.Email, senhaHash);

            if (usuario is null)
            {
                throw new Exception("Usuário ou senha inválidos.");
            }

            var token = _autenticacaoService.GerarJwtToken(usuario.Email, usuario.Nome, usuario.Role.ToString(), usuario.Id);

            return token;
        }
    }
}
