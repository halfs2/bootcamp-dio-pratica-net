using System;
using FluentValidation.Results;

namespace DIO.Series.Entity
{ 
    public abstract class EntityBase
    {
        protected ValidationResult ValidationResult;
        
        public int Id { get; private set; }

        public EntityBase(int id)
        {
            Id = id;
            ValidationResult = new ValidationResult();
        }
         protected void AdicionarErro(string mensagem)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
        }
    }
}