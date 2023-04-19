﻿using System.ComponentModel.DataAnnotations;

namespace AdoPet.Models;

/// <summary>
/// Classe mãe, responsavel pelo mapeamento dos campos, para inserção ou consulta ao banco.
/// </summary>
public class ReadAdocaoDto
{
    /// <summary>
    /// ID do tutor cadastrado no banco
    /// </summary>
    [Key]
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// Nome do Tutor
    /// </summary>
    public int Pet { get; set; }

    /// <summary>
    /// Nome do Tutor
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Nome do Tutor
    /// </summary>
    public DateTime Date { get; set; }

}
