
using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalogo.InputModel;
using Catalogo.ViewModel;
using Catalogo.services;
using System.ComponentModel.DataAnnotations;
using Catalogo.Exceptions;

namespace Catalogo.Controllers
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class JogoController: ControllerBase
    {
        //Não é responsabilidade do JogoController criar ela. NO caso AspNet Irá cria-la (em Startup)
        private readonly IJogoService _jogoservice;

        public JogoController (IJogoService jogoservice)
        {
            _jogoservice = jogoservice;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoViewModel>>> Obter([FromQuery, Range(1,int.MaxValue)] int pagina = 1,[FromQuery, Range(1,50)] int quantidade = 5)
        {

            var jogos = await _jogoservice.Obter(pagina,quantidade);

            if(jogos.Count() == 0)
                return NoContent();
            
            return Ok(jogos);
        }

        [HttpGet("{idJogo:guid}")]
        public async Task<ActionResult<List<JogoViewModel>>> Obter([FromRoute] Guid idJogo)
        {
            var jogo = await _jogoservice.Obter(idJogo);

            if(jogo == null)
                return NoContent();

            return Ok(jogo);
        }


       /// <summary>
        /// Inserir um jogo no catálogo
        /// </summary>
        /// <param name="jogoInputModel">Dados do jogo a ser inserido</param>
        /// <response code="200">Cao o jogo seja inserido com sucesso</response>
        /// <response code="422">Caso já exista um jogo com mesmo nome para a mesma produtora</response>   
        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> InserirJogo([FromBody] JogoInputModel jogoInputModel)
        {
            try
            {
                var jogo = await _jogoservice.Inserir(jogoInputModel);

                return Ok(jogo);
            }
            catch (JogoJaCadastradoException ex)
            {
                return UnprocessableEntity("Já existe um jogo com este nome para esta produtora");
            }
        }


        [HttpPut("{idJogo:Guid}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idJogo, [FromBody] JogoInputModel jogo)
        {
            try{
                await _jogoservice.Atualizar(idJogo,jogo);
                return Ok();
            }
            catch(JogoJaCadastradoException)
            {
                return NotFound("Não existe esse jogo!");
            }
        }


        [HttpPatch("{idJogo:Guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idJogo,[FromRoute] double preco)
        {
            try
            {
                await _jogoservice.Atualizar(idJogo,preco);
            return Ok();
            }
             catch(JogoJaCadastradoException )
            {
                return NotFound("Não existe este jogo");
            }
        }


        [HttpDelete("{idJogo:Guid}")]
        public async Task<ActionResult> ApagarJogo([FromRoute] Guid idJogo)
        {
            try
            {
                await _jogoservice.Delete(idJogo);
                return Ok();
            }        
            catch(JogoJaCadastradoException)
            {
                return NotFound("Não existe este jogo");
            }   
        }


    }



}