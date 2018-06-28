
namespace Adventure.Language
{
    /// <summary>
    /// Directional flags used by <see cref="PrepositionNode"/> objects.
    /// Each represents a physical direction or relative location.
    /// </summary>
    public enum PrepFlag
    {
        North, East, South, West,
        Northeast, Northwest, Southeast, Southwest,
        Up, Down, In, Out,
        On, Under, At
    }
}