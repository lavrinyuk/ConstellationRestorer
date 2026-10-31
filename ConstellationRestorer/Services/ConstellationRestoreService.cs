namespace ConstellationRestorer.Services;

public class ConstellationRestoreService : IConstellationRestoreService
{
    public string GetMissingSymbols(string reference, string damaged)
    {
        var missing = new List<char>();

        for (int i = 0; i < reference.Length; i++)
        {
            bool found = false;

            for (int j = 0; j < damaged.Length; j++)
            {
                if (reference[i] == damaged[j])
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                missing.Add(reference[i]);
            }
        }

        missing.Sort();

        return new string(missing.ToArray());
    }
}
