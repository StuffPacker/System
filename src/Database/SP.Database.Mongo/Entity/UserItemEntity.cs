namespace SP.Database.Mongo.Entity;

public class UserItemEntity : BaseEntity
{
    public string Name { get; set; }

    public string UserId { get; set; }

    public string WeightSufix { get; set; }

    public decimal Weight { get; set; }
}