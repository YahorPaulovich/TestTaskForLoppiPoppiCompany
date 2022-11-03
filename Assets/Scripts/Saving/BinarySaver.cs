using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BinarySaver
{
    private readonly static string _fileName = "ArithmeticExpression";
    private readonly static string _extension = ".dat";
    private readonly static string _directory = "Saves";

    private readonly static string _fullDirectoryPath = Application.persistentDataPath + "/" + _directory;
    private readonly static string _fullFilePath = Application.persistentDataPath + "/" + _directory + "/" + _fileName + _extension;

    private readonly BinaryFormatter _formatter;

    public BinarySaver()
    {
        _formatter = new BinaryFormatter();
    }

    public void Save(ArithmeticExpression data)
    {
        if (!DirectoryExists())
        {
            Directory.CreateDirectory(_fullDirectoryPath);
        }

        using (FileStream fileStream = new FileStream(_fullFilePath, FileMode.OpenOrCreate))
        {
            _formatter.Serialize(fileStream, data);
        }       
    }

    public ArithmeticExpression Load()
    {
        if (SaveExists())
        {
            try
            {
                using (FileStream fileStream = File.Open(_fullFilePath, FileMode.Open))
                {
                    return (ArithmeticExpression)_formatter.Deserialize(fileStream);
                }
            }
            catch (SerializationException ex)
            {
                Debug.Log("Failed to load file: " + ex.Message);
            }
        }

        return new ArithmeticExpression();
    }

    public void DeleteSave()
    {
        if (File.Exists(_fullFilePath))
        {
            File.Delete(_fullFilePath);
        }
    }

    private static bool DirectoryExists()
    {
        return Directory.Exists(_fullDirectoryPath);
    }

    private static bool SaveExists()
    {
        return File.Exists(_fullFilePath);
    }
}
