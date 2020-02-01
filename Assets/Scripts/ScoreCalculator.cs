// POINTS
// Category = 2
// Colour = 1
// Type = 1
public class ScoreCalculator {
    public int CalculateScore (BodyPartItem realItem, BodyPartItem selectedItem) {
        int category = realItem.Category == selectedItem.Category ? 2 : 0;
        int colour = realItem.Colour == selectedItem.Colour ? 1 : 0;
        int type = realItem.Type == selectedItem.Type ? 1 : 0;
        return category + colour + type;
    }
}