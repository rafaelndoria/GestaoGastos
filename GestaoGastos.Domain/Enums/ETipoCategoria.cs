namespace GestaoGastos.Domain.Enums
{
    public enum ETipoCategoria
    {
        // Gastos Essenciais
        Alimentacao = 1,
        Moradia = 2,
        ContasDomesticas = 3,
        Transporte = 4,
        Saude = 5,
        Educacao = 6,

        // Gastos Não Essenciais
        Lazer = 7,
        Compras = 8,
        Viagem = 9,
        Assinaturas = 10,
        Pets = 11,

        // Dívidas e Obrigações
        Emprestimos = 12,
        Financiamentos = 13,
        CartaoCredito = 14,
        Impostos = 15,

        // Receitas
        Salario = 50,
        Rendimentos = 51,
        Bonus = 52,
        Vendas = 53,
        Reembolsos = 54,
        Aposentadoria = 55,

        // Investimentos
        InvestimentosAcoes = 100,
        InvestimentosRendaFixa = 101,
        InvestimentosCripto = 102,
        Poupanca = 103,
        Dividendos = 104
    }
}
