using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoTesteLuiz.Models
{
    public class Pessoa
    {
        private Pessoa() { }
        public Pessoa(int id, string? nome, string? cpf, string? telefone, char sexo, DateTime dataNascimento, DateTime dataHoraCadastro)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Telefone = telefone;
            Sexo = sexo;
            DataNascimento = dataNascimento;
            DataHoraCadastro = dataHoraCadastro;
        }

        public int Id { get; private set; }
        public string? Nome { get; private set; }
        public string? Cpf { get; private set; }
        public string? Telefone { get; private set; }
        public char Sexo { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public DateTime DataHoraCadastro { get; private set; }
    }
}
