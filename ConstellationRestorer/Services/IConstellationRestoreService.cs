namespace ConstellationRestorer.Services;

public interface IConstellationRestoreService
{
    string GetMissingSymbols(string reference, string damaged);
}
