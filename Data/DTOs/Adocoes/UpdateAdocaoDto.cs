﻿using System.ComponentModel.DataAnnotations;

namespace AdoPet.Models;

/// <summary>
/// Classe mãe, responsavel pelo mapeamento dos campos, para inserção ou consulta ao banco.
/// </summary>
public class UpdateAdocaoDto
{
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
    [Required(ErrorMessage = "A Data de Adoção é obrigatória!")]
    public DateTime Date { get; set; }

}
