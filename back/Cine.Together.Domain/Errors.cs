namespace Cine.Together.Domain;

public static class Errors
{
    public static class AggregateNotFound
    {
        public const string CODE = "AGGREGATE_NOT_FOUND";
        public const string MESSAGE = "Aggregate does not exists";
    }
}
