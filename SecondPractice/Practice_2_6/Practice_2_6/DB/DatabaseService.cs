using Npgsql;

namespace Practice_2_6.DB;

/// <summary>
/// Класс DatabaseService
/// отвечает за подключение и открытие соединения с БД 
/// </summary>
public static class DatabaseService
{
    public const int NOT_LOGIN = -1;
    /// <summary>
    /// Id вошедшего пользователя
    /// </summary>
    public static int idLoginUser = NOT_LOGIN;
    
    /// <summary>
    /// Переменная _connection
    /// хранит открытое соединение с БД
    /// </summary>
    private static NpgsqlConnection? _connection;
    /// <summary>
    /// Метод GetConnectionString()
    /// возвращает строку подключения к БД
    /// </summary>
    private static string GetConnectionString()
    {
        return @"Host=localhost;Port=5432;Database=todo_db;Username=admin_garage_db;Password=0admin12345";
    }

    /// <summary>
    /// Метод GetSqlConnection()
    /// проверяет есть ли уже открытое соединение с БД
    /// если нет, то открывает соединение с БД
    /// </summary>
    /// <returns></returns>
    public static NpgsqlConnection GetSqlConnection()
    {
        if (_connection is null)
        {
            _connection = new NpgsqlConnection(GetConnectionString());
            _connection.Open();
        }

        return _connection;
    }
}