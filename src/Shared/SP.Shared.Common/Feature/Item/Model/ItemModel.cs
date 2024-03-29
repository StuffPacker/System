using System.Globalization;

namespace SP.Shared.Common.Feature.Item.Model;

public class ItemModel
{
    public string Name { get; set; } = string.Empty;

    public string Id { get; set; } = string.Empty;

    public decimal Weight { get; set; }

    public string WeightSufix { get; set; } = string.Empty;

    public Guid UserId { get; set; }

    public string Description { get; set; } = string.Empty;

    public void ChangeWeight(string weight)
    {
        weight = weight.Replace(",", ".");
        Weight = Convert.ToDecimal(weight, CultureInfo.InvariantCulture);
        Weight = Math.Round(Weight, 2);
    }
}