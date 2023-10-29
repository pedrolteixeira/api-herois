using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class HeroiController : ControllerBase
    {
        private DataContext dc;

        public HeroiController(DataContext context)
        {
            this.dc = context;
        }

        /// <summary>
        /// Cadastra um Héroi.
        /// </summary>
        /// <param name="h">Heroi para cadastrar</param>
        /// <response code="200">O heroi foi cadastrado corretamente</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost("api")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> cadastrar([FromBody] Heroi h)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { código = 400, menssagem = "Preencha todos os campos corretamente" });
                }

                var existeHeroi = dc.heroi.FirstOrDefault(heroi => heroi.NomeHeroi == h.NomeHeroi);
                if (existeHeroi != null)
                {
                    return BadRequest(new { código = 400, menssagem = "Um herói com o mesmo nome já existe" });
                }

                dc.heroi.Add(h);
                await dc.SaveChangesAsync();
                return Created("heroi", new { código = 200, menssagem = "Herói cadastrado com sucesso", data = h });
            }
            catch (Exception ex)
            {
                return BadRequest(new { código = 400, menssagem = "Erro ao cadastrar heroi: " + ex.Message });
            }
        }



        /// <summary>
        /// Atualiza um Herói existente.
        /// </summary>
        /// <param name="h">Herói para atualizar.</param>
        /// <returns> Herói atualizado</returns>
        /// <response code="200"> Herói atualizado com sucesso</response>
        /// <response code="400"> Erro ao atualizar o herói</response>
        [HttpPut("api")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
    public async Task<ActionResult> Editar([FromBody] Heroi h)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { código = 400, menssagem = "Preencha todos os campos corretamente" });
            }

            var existeOutroHeroi = dc.heroi.Any(heroi => heroi.NomeHeroi == h.NomeHeroi && heroi.Id != h.Id);

            if (existeOutroHeroi)
            {
                return BadRequest(new { código = 400, menssagem = "Um herói com o mesmo nome já existe" });
            }

            dc.heroi.Update(h);
            await dc.SaveChangesAsync();

            return Ok(new { código = 200, menssagem = "Herói atualizado com sucesso", data = h });
        }
        catch (Exception ex)
        {
            return BadRequest(new { código = 400, menssagem = "Ocorreu um erro ao atualizar herói: " + ex.Message });
        }
    }

        /// <summary>
        /// Exclui um Herói pelo ID.
        /// </summary>
        /// <param name="id">ID do Herói para excluir.</param>
        /// <returns> Herói excluído corretamente</returns>
        /// <response code="200"> Herói excluído</response>
        /// <response code="400"> Id inválido</response>
        /// <response code="404"> Herói não encontrado</response>
        [HttpDelete("api/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> excluir(int id)
        {
            try
            {
                var heroi = await dc.heroi.FindAsync(id);
                if (heroi == null)
                {
                    return NotFound(new { código = 404, menssagem = "Herói não encontrado" });
                }

                dc.heroi.Remove(heroi);
                await dc.SaveChangesAsync();

                return Ok(new { código = 204, menssagem = "Herói excluído com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { código = 400, menssagem = "Ocorreu um erro ao excluir o herói: " + ex.Message });
            }
        }


        /// <summary>
        /// Lista todos os Herois cadastrados.
        /// </summary>
        /// <returns>Lista de Heróis cadastrados</returns>
        /// <response code="200">Lista de Heróis cadastrados</response>
        [HttpGet("api")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> listar()
        {
            try
            {
                var dados = await dc.heroi.ToListAsync();
                return Ok(new { código = 200, menssagem = "Heróis listados com sucesso", data = dados });
            }
            catch (Exception ex)
            {
                return BadRequest(new { código = 400, menssagem = "Ocorreu um erro ao listar os heróis: " + ex.Message });
            }
        }

        /// <summary>
        /// Filtra um Herói pelo ID.
        /// </summary>
        /// <param name="id">ID do Herói para filtrar.</param>
        /// <returns>Retorna o Herói encontrado.</returns>
        /// <response code="200"> Herói encontrado</response>
        /// <response code="400"> Id inválido</response>
        /// <response code="404"> Herói não encontrado</response>
        [HttpGet("api/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult filtrar(int id)
        {
            try
            {
                Heroi h = dc.heroi.Find(id);
                if (h == null)
                {
                    List<Heroi> todosHerois = dc.heroi.ToList();
                    return NotFound(new { código = 404, menssagem = "Herói não encontrado", allHeroes = todosHerois });
                }

                return Ok(new { código = 200, menssagem = "Herói encontrado", data = h });
            }
            catch (Exception ex)
            {
                return BadRequest(new { código = 400, menssagem = "Ocorreu um erro ao filtrar o herói: " + ex.Message });
            }
        }
    }
}