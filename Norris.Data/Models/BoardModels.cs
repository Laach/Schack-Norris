namespace Norris.Data.Models.Board{
  public enum Char{
    A, B, C, D, E, F, G, H
  }

  public enum Num{
    One, Two, Three, Four, Five, Six, Seven, Eight
  }

  public struct PositionModel{
    Char Char;
    Num Num;
  }

  public struct MoveModel{
    PositionModel From;
    PositionModel To;
  }

  public enum PieceType{
    Pawn,
    Knight,
    Bishop,
    Rook,
    Queen,
    King
  }

  public enum Color{
    White,
    Black
  }

  public class PieceModel{
    public PieceType Type;
    public Color Color;
  }

  public class Tile{
    public PieceModel Piece {get; set;}
    public bool IsSelected  {get; set;}
    public bool SelectedCanMoveTo   {get; set;}
    public bool SelectedCanTake {get; set;}
  }

  public class BoardModel{
    public Tile[,] Board;
  }

}