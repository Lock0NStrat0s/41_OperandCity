namespace OperandCity;

class Program
{
    static void Main(string[] args)
    {
        BlockCoordinate first = new (10, 10);
        BlockOffset oFive = new(5, 5);

        Console.WriteLine(first + oFive);
        Console.WriteLine(first + Direction.East);
        Console.WriteLine(first[0] + ", " + first[1]);
        
        BlockOffset north = Direction.North;
        Console.WriteLine(first + north);
    }
}

public record BlockCoordinate(int Row, int Column)
{
    public static BlockCoordinate operator +(BlockCoordinate bc, BlockOffset bo) =>
        new BlockCoordinate(bc.Row + bo.RowOffset, bc.Column + bo.ColumnOffset);

    public static BlockCoordinate operator +(BlockCoordinate bc, Direction d)
    {
        BlockCoordinate bc2 = d switch
        {
            Direction.North => new BlockCoordinate(bc.Row - 1, bc.Column),
            Direction.West => new BlockCoordinate(bc.Row, bc.Column - 1),
            Direction.South => new BlockCoordinate(bc.Row + 1, bc.Column),
            Direction.East => new BlockCoordinate(bc.Row, bc.Column + 1),
            _ => new BlockCoordinate(bc.Row, bc.Column)
        };
        
        return bc2;
    }
    
    public int this[int index]
    {
        get
        {
            if (index == 0) return Row;
            if (index == 1) return Column;
            throw new IndexOutOfRangeException();
        }
    }
}

public record BlockOffset(int RowOffset, int ColumnOffset)
{
    public static implicit operator BlockOffset(Direction d)
    {
        BlockOffset bo = d switch
        {
            Direction.North => new BlockOffset(-1, 0),
            Direction.West => new BlockOffset(0, -1),
            Direction.South => new BlockOffset(1, 0),
            Direction.East => new BlockOffset(0, 1),
            _ => new BlockOffset(0,0)
        };

        return bo;
    }
}
public enum Direction { North, East, South, West }