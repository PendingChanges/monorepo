namespace BetterNote.Application.Tags;
public interface IReadTags
{
    Task<IReadOnlyList<TagDocument>> GetAllTagAsync(CancellationToken cancellationToken = default);
}
