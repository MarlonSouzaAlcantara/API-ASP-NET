using System.ComponentModel.DataAnnotations;
using System;



namespace Catalogo.InputModel
{
    public class JogoInputModel
    {
        [Required]
        [StringLength(100,MinimumLength = 3 , ErrorMessage = "O nome do jogo deve conter entre  3 e 100 caracteres")]
        public string Nome {get;set;}

         [Required]
        [StringLength(100,MinimumLength = 3 , ErrorMessage = "O nome da produtora deve conter entre  3 e 100 caracteres")]
        public string Produtora {get;set;}
        
         [Required]
        [Range(1,1000  , ErrorMessage = "O nome da produtora deve conter entre  3 e 100 caracteres")]
        public double preco {get;set;}


    }
}