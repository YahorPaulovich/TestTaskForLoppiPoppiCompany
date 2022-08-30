public struct BoxData
{
    public int Index;
    public Mark Mark;
    public bool IsMarked;

    public BoxData(int index = 0, Mark mark = Mark.None, bool isMarked = false)
    {
        Index = index;
        Mark = mark;
        IsMarked = isMarked;
    }

    public override string ToString()
    {
        return "Index: " + Index + "; Mark: " + Mark + "; IsMarked: " + IsMarked;
    }
}
