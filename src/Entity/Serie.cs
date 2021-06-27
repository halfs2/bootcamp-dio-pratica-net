using System;
using System.Collections.Generic;
using System.Linq;
using DIO.Series.Enums;
using FluentValidation;

namespace DIO.Series.Entity
{
    public class Serie : EntityBase
    {
        public string Titulo { get; private set; }  
        public string Descricao { get; private set; }
        public int Ano { get; private set; }
        public Genero Genero { get; private set; }
        
        public Serie(int id, string titulo, string descricao, int ano, Genero genero) : base(id)
        {
            Titulo = titulo;
            Descricao = descricao;
            Ano = ano;
            Genero = genero;
        }

        public bool IsValid()
        {
            SerieValidator validator = new SerieValidator();
            
            var result = validator.Validate(this);
            
            foreach(var erro in result.Errors)
            {
                AdicionarErro(erro.ErrorMessage);
            }

            return result.IsValid;
        }

        public IEnumerable<string> ObterErros()
        {
            return ValidationResult.Errors.Select(e => e.ErrorMessage).ToList();
        }

        public override string ToString()
		{
            string retorno = "";
            retorno += "Gênero: " + this.Genero + Environment.NewLine;
            retorno += "Titulo: " + this.Titulo + Environment.NewLine;
            retorno += "Descrição: " + this.Descricao + Environment.NewLine;
            retorno += "Ano de Início: " + this.Ano + Environment.NewLine;
			return retorno;
		}
        class SerieValidator : AbstractValidator<Serie>
        {
            public SerieValidator()
            {
                RuleFor(s => s.Id)
                    .NotEmpty();

                RuleFor(s => s.Titulo)
                    .NotNull()
                    .MinimumLength(4);
                
                RuleFor(s => s.Descricao)
                    .NotNull()
                    .MinimumLength(4);
                
                RuleFor(s => s.Ano)
                    .NotEmpty();
                
                RuleFor(s => s.Genero)
                    .IsInEnum();
            }
        }
    }
}