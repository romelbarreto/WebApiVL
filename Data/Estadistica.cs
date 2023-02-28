using System;
using System.Collections.Generic;

namespace WebApiVL.Models;

public partial class Estadistica
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public DateOnly Fecha { get; set; }

    public TimeOnly Velocidad { get; set; }

    public int CantidadPalabras { get; set; }

    public int IdNivel { get; set; }

    public int IdTarjeta { get; set; }

    public virtual Nivel IdNivelNavigation { get; set; } = null!;

    public virtual Tarjeta IdTarjetaNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
