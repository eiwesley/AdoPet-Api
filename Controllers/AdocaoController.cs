﻿using AdoPet.Models;
using AutoMapper;
using Data.DTOs.Abrigos;
using Microsoft.AspNetCore.Mvc;
using Models.Data;
using Models.Models;
using AdoPet.Controllers;
using Data.DTOs.Pets;
using Data.DTOs.Tutor;


namespace AdoPet.Controllers;

/// <summary>
/// Classe Responsavel por controlar os requisitos da API
/// </summary>
[ApiController]
[Route("[Controller]")]
public class AdocaoController : Controller
{
    private AdoPetContext _context;
    private IMapper _mapper;

    /// <summary>
    /// Metodo construtor
    /// </summary>
    /// <param name="context">retorna os dados da classe</param>
    /// <param name="mapper">Mapeia os dados para os DTOs</param>
    public AdocaoController(AdoPetContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona uma adocao ao banco de dados
    /// </summary>
    /// <param name="adocaoDto">Objeto com os campos necessários para criação de um tutor</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    public IActionResult CadastrarAdocao([FromBody] CreateAdocaoDto adocaoDto)
    {
        Adocao adocao = _mapper.Map<Adocao>(adocaoDto);
        _context.Adocao.Add(adocao);
        _context.SaveChanges();

        // retorna o nome do Tutor cadastrado, de acordo com o ID informado
        var tutor = _context.User.FirstOrDefault(tutor => tutor.Id == adocaoDto.UserId);
        if (tutor == null) return NotFound();

        // atualiza o status e tutor, de acordo com o ID informado
        var pet = _context.Pet.FirstOrDefault(pet => pet.Id == adocaoDto.Pet);
        if (pet == null) return NotFound();
        var petParaAdotar = _mapper.Map<UpdatePetDto>(pet);
        petParaAdotar.Status = "Adopted";
        petParaAdotar.UserId = tutor.Id;

        _mapper.Map(petParaAdotar, pet);
        _context.SaveChanges();

        return Ok(pet);

    }



    /// <summary>
    /// Deleta o cadastro de um tutor especifico
    /// </summary>
    /// <param name="id">Id da Adocao</param>
    /// <param name="idTutor">ID do tutor cadastrado no banco</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso a alteração seja feita com sucesso</response>
    [HttpDelete("{id}")]
    public IActionResult DeletarAdocao(int id, [FromQuery] int idTutor = 0)
    {

        // retorna o nome do Tutor cadastrado, de acordo com o ID informado
        var tutor = _context.User.FirstOrDefault(tutor => tutor.Id == idTutor);
        if (tutor == null) return NotFound();
        var tutorProfile = _mapper.Map<ReadTutorDto>(tutor).Profile;
        if ((tutorProfile == "Admin") || (tutorProfile == "Abrigo"))
        {
            var adocao = _context.Adocao.FirstOrDefault(adocao => adocao.Id == id);

            if (adocao == null) return NotFound();

            _context.Remove(adocao);
            _context.SaveChanges();

            var pet = _context.Pet.FirstOrDefault(pet => pet.Id == adocao.Pet);
            if (pet == null) return NotFound();
            var petParaAdotar = _mapper.Map<UpdatePetDto>(pet);
            petParaAdotar.Status = "Suspended";
            petParaAdotar.UserId = 0;

            _mapper.Map(petParaAdotar, pet);
            _context.SaveChanges();

            return NoContent();
        }
        else
        {
            return BadRequest();
        }




    }

}
