
namespace Adventure.Controller
{
    /// <summary>
    /// Valid directional codes used by <see cref="DirectionNode"/> objects.
    /// Each represents a physical direction.
    /// </summary>
    public enum DirCode
    {
        North, East, South, West,
        Northeast, Northwest, Southeast, Southwest,
        Up, Down, In, Out
    }
}