using DA.Lang.Models;
using Microsoft.Data.Sqlite;

namespace DA.Lang.Database;

public class LangDatabase
{
    string _connectionString;
    
    public LangDatabase(string connectionString)
    {
        _connectionString = connectionString;
        this.Initialize();
    }
    
    private void Initialize()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        
        using var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Settings (
                Id INTEGER PRIMARY KEY,
                OpenAiKey TEXT,
                DefaultOutputLanguage TEXT
            );

            CREATE TABLE IF NOT EXISTS Translation (
                Id INTEGER PRIMARY KEY,
                OriginalText TEXT,
                TranslatedText TEXT,
                Tone INTEGER,
                Language TEXT,
                TranslationService TEXT
            );
        ";
        
        command.ExecuteNonQuery();
    }

    public async Task UpsertSettingsAsync(Settings settings)
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        
        using var command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO Settings (Id, OpenAiKey, DefaultOutputLanguage)
            VALUES (1, $openAiKey, $defaultOutputLanguage)
            ON CONFLICT(Id) DO UPDATE SET
                OpenAiKey = $openAiKey,
                DefaultOutputLanguage = $defaultOutputLanguage;
        ";
        
        command.Parameters.AddWithValue("$openAiKey", settings.OpenAiKey);
        command.Parameters.AddWithValue("$defaultOutputLanguage", settings.DefaultOutputLanguage);
        
        await command.ExecuteNonQueryAsync();
    }
    
    public Settings GetSettings()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Settings WHERE Id = 1;";
        
        using var reader = command.ExecuteReader();
        if (!reader.Read())
        {
            return new Settings();
        }
        
        return new Settings
        {
            Id = reader.GetInt32(0),
            OpenAiKey = reader.GetString(1),
            DefaultOutputLanguage = reader.GetString(2)
        };
    }
}