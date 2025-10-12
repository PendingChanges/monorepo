namespace RPG.Domain.Jobs;

public class JobCollection
{
    private readonly List<Job> _jobs = new();

    public IReadOnlyList<Job> Jobs => _jobs.AsReadOnly();

    public JobCollection()
    {
    }

    public JobCollection(params Job[] jobs)
    {
        _jobs.AddRange(jobs);
    }

    public void Add(Job job)
    {
        _jobs.Add(job);
    }

    public Job? GetJobByName(string name)
        => _jobs.FirstOrDefault(j => j.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

    public bool HasJob(string name)
        => _jobs.Any(j => j.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
}
