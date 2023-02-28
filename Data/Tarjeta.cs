using System;
using System.Collections.Generic;

namespace WebApiVL.Models;

public partial class Tarjeta
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string Cuerpo { get; set; } = null!;

    public string Enlace { get; set; } = null!;

    public string Pregunta1 { get; set; } = null!;

    public string Pregunta2 { get; set; } = null!;

    public string Pregunta3 { get; set; } = null!;

    public string Op1P1 { get; set; } = null!;

    public string Op2P1 { get; set; } = null!;

    public string Op3P1 { get; set; } = null!;

    public string Op4P1 { get; set; } = null!;

    public string Op5P1 { get; set; } = null!;

    public string Dificultad { get; set; } = null!;

    public DateOnly Fecha { get; set; }

    public int Palabras { get; set; }

    public string Op1P2 { get; set; } = null!;

    public string Op2P2 { get; set; } = null!;

    public string Op3P2 { get; set; } = null!;

    public string Op4P2 { get; set; } = null!;

    public string Op5P2 { get; set; } = null!;

    public string Op1P3 { get; set; } = null!;

    public string Op2P3 { get; set; } = null!;

    public string Op3P3 { get; set; } = null!;

    public string Op4P3 { get; set; } = null!;

    public string Op5P3 { get; set; } = null!;

    public string Resp1 { get; set; } = null!;

    public string Resp2 { get; set; } = null!;

    public string Resp3 { get; set; } = null!;

    public string Genero { get; set; } = null!;
}
