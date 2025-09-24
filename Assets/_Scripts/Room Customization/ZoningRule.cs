[System.Serializable]
public class ZoningRule {
    public RoomType requiredRoom;
    public RoomType neighborRoom;
    public bool mustBeAdjacent;
}