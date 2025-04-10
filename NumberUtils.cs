namespace StatsRandomizer;

// Just an old number utils class that I've used around different projects.
// Cleaned up and trimmed down to only methods that are actually used... Which is just the Next() method... :wilt:
internal static class NumberUtils
{
    /// <summary>
    /// Represents a shared instance of a random number generator.
    /// </summary>
    internal static readonly System.Random random = new(GenerateTrulyRandomNumber());

    /// <summary>
    /// Generates a truly random number using cryptographic random number generation.
    /// </summary>
    /// <returns>A truly random number within a specified range.</returns>
    internal static int GenerateTrulyRandomNumber()
    {
        using System.Security.Cryptography.RNGCryptoServiceProvider rng = new();
        byte[] bytes = new byte[4]; // 32 bities :3c
        rng.GetBytes(bytes);

        // Convert the random bytes to an integer and ensure it falls within the specified range
        int randomInt = System.BitConverter.ToInt32(bytes, 0);
        return System.Math.Abs(randomInt % (50 - 10)) + 10;
    }

    /// <summary>
    /// Returns a random float number within the specified range.
    /// </summary>
    /// <param name="min">The inclusive lower bound of the random float number to be generated.</param>
    /// <param name="max">The exclusive upper bound of the random float number to be generated.</param>
    /// <returns>A random float number within the specified range.</returns>
    internal static float Next(float min, float max) => (float)((NextD() * (max - min)) + min);

    /// <summary>
    /// Returns a random double number between 0.0 and 1.0.
    /// </summary>
    /// <returns>A random double number between 0.0 and 1.0... Duh</returns>
    internal static double NextD() => random.NextDouble();
}