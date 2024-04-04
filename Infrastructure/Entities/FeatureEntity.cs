namespace Infrastructure.Entities;

public class FeatureEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string ingress { get; set; } = null!;
    
    public ICollection<FeatureItemEntity> FeatureItems { get; set; } = [];
}