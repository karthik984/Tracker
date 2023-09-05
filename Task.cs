// See https://aka.ms/new-console-template for more information
public class Task
{
    public int Id { get; set; }
    public string Name {get; set;}
    public string Description { get; set; }

    public DateTime Due { get; set; }
    public DateTime LastUpdated {get; set; }
    public DateTime? Done { get; set; }

    public Task()
    {
        
    }
    public Task(int id, string name, string description, DateTime due)
    {
        this.Id = id;
        this.Name = name;
        this.Due = due;
        this.LastUpdated = DateTime.Now;
        this.Description = description;
        this.Done = null;
    }
}