using MVCBasico.Models;

public enum Moneda
{
    [SignoMoneda("$")]
    PESOS,
    [SignoMoneda("U$S")]
    DOLARES
}