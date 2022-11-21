using VetClinic.Interfaces;

namespace VetClinic.Modules;

public static class ClinicBackup
{
    /// <summary>
    /// Reads an <see cref="IClinic"/> instance from a binary file.
    /// </summary>
    /// <param name="path">The file path to read the object instance from.</param>
    /// <returns>Returns a new instance of the object read from the binary file.</returns>
    public static IClinic Import(string path)
    {
        using Stream stream = File.Open(path, FileMode.Open);
        var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        return (IClinic)binaryFormatter.Deserialize(stream);
    }

    /// <summary>
    /// Writes the given <see cref="IClinic"/> instance to a binary file.
    /// </summary>
    /// <param name="clinic"></param>
    /// <param name="path">The file path to write the object instance to.</param>
    /// <returns></returns>
    public static string Export(IClinic clinic, string? path = null)
    {
        path += $"{clinic.Name}_{clinic.GetType()}_{DateTime.Now.Ticks}.clinic.backup";
        foreach (char c in Path.GetInvalidPathChars())
            path = path.Replace(c, '_');
        path = path.Replace(' ', '-');

        using Stream stream = File.Open(path, FileMode.Create);
        var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        binaryFormatter.Serialize(stream, clinic);

        return path;
    }
}