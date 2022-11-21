namespace VetClinic.Interfaces;

public interface IMenuItem
{
    public string Description { get; set; }
    public Action Execute { get; set; }
}