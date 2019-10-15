using System;
namespace Norris.Game {

  struct Point{
    public int Y {get; set;}
    public int X {get; set;}

    public override bool Equals(object obj)
    {
      return obj is Point point &&
            Y == point.Y &&
            X == point.X;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public override string ToString()
    {
      return $"X = {X} | Y = {Y}";
    }

    public static bool operator ==(Point a, Point b){
      return a.X == b.X && a.Y == b.Y; 
    }

    public static bool operator !=(Point a, Point b){
      return a.X != b.X || a.Y != b.Y; 
    }
  }

}