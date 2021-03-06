using System.Collections.Generic;
using System.Linq;
using Repository;
using Microsoft.EntityFrameworkCore;
using System.Windows.Forms;
using System;
using System.ComponentModel.DataAnnotations;
using Repository;

namespace Models
{
  public class Quarto
  {
    public int Id { get; set; }
    public int NumeroQuarto { get; set; }
    public int Andar { get; set; }
    public string Descricao { get; set; }
    public string Reservado { get; set; }
    public double Valor { get; set; }

    public Quarto() { }

    public Quarto(
        int NumeroQuarto,
        int Andar,
        string Descricao,
        string Reservado,
        double Valor
    )
    {
      this.NumeroQuarto = NumeroQuarto;
      this.Andar = Andar;
      this.Descricao = Descricao;
      this.Reservado = Reservado;
      this.Valor = Valor;

      Context db = new Context();
      db.Quartos.Add(this);
      db.SaveChanges();
    }

    public override string ToString()
    {
      return $"\n ---------------------------------------"
          + $"\n ID: {this.Id}"
          + $"\n Número do Quarto: {this.NumeroQuarto}"
          + $"\n Andar: {this.Andar}"
          + $"\n Descrição: {this.Descricao}"
          + $"\n Status da Reserva: {this.Reservado}"
          + $"\n Valor: {this.Valor}";
    }

    public override bool Equals(object obj)
    {
      if (obj == null)
      {
        return false;
      }

      if (!Quarto.ReferenceEquals(this, obj))
      {
        return false;
      }
      Quarto it = (Quarto)obj;
      return it.Id == this.Id;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public static void AtualizaReservado(
        int Id,
        string Reservado
    )
    {
      Quarto quartoReservado = GetQuarto(Id);

      quartoReservado.Reservado = Reservado;

      Context db = new Context();
      db.Quartos.Update(quartoReservado);
      db.SaveChanges();
    }

    public static void AlterarQuarto(
        int Id,
        int NumeroQuarto,
        int Andar,
        string Descricao,
        string Reservado,
        double Valor
    )
    {
      Quarto quarto = GetQuarto(Id);
      quarto.NumeroQuarto = NumeroQuarto;
      quarto.Andar = Andar;
      quarto.Descricao = Descricao;
      quarto.Reservado = Reservado;
      quarto.Valor = Valor;

      Context db = new Context();
      db.Quartos.Update(quarto);
      db.SaveChanges();
    }

    /*
        public static IEnumerable<Quarto> GetQuartos()
        {
          Context db = new Context();
          return (from Quarto in db.Quartos select Quarto);
        }
    */
    public static List<Quarto> GetQuartos()
    {
      Context db = new Context();
      return (from Quarto in db.Quartos
              where Quarto.Reservado == "N"
              select Quarto).ToList();
    }

    public static Quarto GetQuarto(int Id)
    {
      Context db = new Context();
      IEnumerable<Quarto> quartos = from Quarto in db.Quartos
                                    where Quarto.Id == Id
                                    select Quarto;

      return quartos.First();
    }

    public static void RemoverQuarto(Quarto quarto)
    {
      Context db = new Context();
      db.Quartos.Remove(quarto);
      db.SaveChanges();
    }
  }
}