using System.Data;
using Npgsql;

namespace Practice_2_6.DB;


/// <summary>
/// Класс DatabaseRequests
/// содержит методы для отправки запросов к БД
/// </summary>
public static class DatabaseRequests
{
    /// <summary>
    /// Метод AddNote
    /// отправляет запрос в БД на добавление заметки для авторизированного пользователя
    /// </summary>
    public static void AddNote(string title, string textTodo, DateTime timeEnd)
    {
        var querySql = $"INSERT INTO todo_task(pk_user, title, text_todo, time_end, complited) " +
                       $"VALUES ({DatabaseService.idLoginUser}, '{title}', '{textTodo}', '{timeEnd: yyyy-MM-dd}', False)";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection()); 
        cmd.ExecuteNonQuery();
    }
    
    /// <summary>
    /// Метод UpdateNote
    /// отправляет запрос в БД на обновлении заметки для авторизированного пользователя
    /// </summary>
    public static void UpdateNote(int pkNote, string title, string textTodo, DateTime timeEnd)
    {
        var querySql = $"UPDATE todo_task " +
                       $"SET title='{title}', text_todo='{textTodo}', time_end='{timeEnd: yyyy-MM-dd}' " +
                       $"WHERE pk={pkNote} AND pk_user={DatabaseService.idLoginUser}";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection()); 
        cmd.ExecuteNonQuery();
    }
    
    /// <summary>
    /// Метод DeleteNote
    /// отправляет запрос в БД на удаление заметки для авторизированного пользователя
    /// </summary>
    public static void DeleteNote(int pkNote)
    {
        var querySql = $"DELETE FROM todo_task WHERE pk={pkNote} AND pk_user={DatabaseService.idLoginUser}";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection()); 
        cmd.ExecuteNonQuery();
    }
    
    /// <summary>
    /// Метод СlearNote
    /// отправляет запрос в БД на очистку заметок для авторизированного пользователя
    /// </summary>
    public static void СlearNote()
    {
        var querySql = $"DELETE FROM todo_task WHERE pk_user={DatabaseService.idLoginUser}";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection()); 
        cmd.ExecuteNonQuery();
    }
    
    /// <summary>
    /// Метод GetQueryNotes
    /// отправляет запрос в БД на получение заметок для авторизированного пользователя
    /// </summary>
    public static NpgsqlDataReader GetQueryNotes()
    {
        var querySql = $"SELECT * FROM todo_task WHERE pk_user={DatabaseService.idLoginUser}";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection()); 
        return cmd.ExecuteReader();
    }
    
    /// <summary>
    /// Метод GetQueryCompletedNotes
    /// отправляет запрос в БД на получение выполненных заметок для авторизированного пользователя
    /// </summary>
    public static NpgsqlDataReader GetQueryCompletedNotes()
    {
        var querySql = $"SELECT * FROM todo_task WHERE pk_user={DatabaseService.idLoginUser} AND complited=True";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection()); 
        return cmd.ExecuteReader();
    }
    
    /// <summary>
    /// Метод GetQueryOverdueNotes
    /// отправляет запрос в БД на получение просроченных заметок для авторизированного пользователя
    /// </summary>
    public static NpgsqlDataReader GetQueryOverdueNotes()
    {
        var querySql = $"SELECT * FROM todo_task WHERE pk_user={DatabaseService.idLoginUser} AND complited=False AND time_end < now()";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection()); 
        return cmd.ExecuteReader();
    }
    
    /// <summary>
    /// Метод GetQueryInCompletedNotes
    /// отправляет запрос в БД на получение не сделанных заметок для авторизированного пользователя
    /// </summary>
    public static NpgsqlDataReader GetQueryInCompletedNotes()
    {
        var querySql = $"SELECT * FROM todo_task WHERE pk_user={DatabaseService.idLoginUser} AND complited=False AND time_end > now()";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection()); 
        return cmd.ExecuteReader();
    }
    
    /// <summary>
    /// Метод GetQueryInCompletedNotes
    /// отправляет запрос в БД на получение не сделанных заметок между двумя датами для авторизированного пользователя
    /// </summary>
    public static NpgsqlDataReader GetQueryNotesOnDateBetweenDates(DateTime start, DateTime end)
    {
        var querySql = $"SELECT * FROM todo_task WHERE pk_user={DatabaseService.idLoginUser} AND complited=False AND " +
                       $"time_end BETWEEN '{start:yyyy-MM-dd}' AND '{end:yyyy-MM-dd}'";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection()); 
        return cmd.ExecuteReader();
    }

    /// <summary>
    /// Метод MarkCompletedNotes
    /// отправляет запрос в БД на то,чтобы пометить заметку как сделанную для авторизированного пользователя
    /// </summary>
    public static void MarkCompletedNotes(int pkNotes)
    {
        var querySql = $"UPDATE todo_task SET complited=True " +
                       $"WHERE pk={pkNotes} AND pk_user={DatabaseService.idLoginUser}";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection()); 
        cmd.ExecuteNonQuery();
    }
    
    /// <summary>
    /// Метод GetNotes
    /// отправляет запрос в БД на получение не сделанных заметок между двумя датами для авторизированного пользователя
    /// </summary>
    public static NpgsqlDataReader GetNotes(int idNotes)
    {
        var querySql = $"SELECT * FROM todo_task WHERE pk={idNotes} AND pk_user={DatabaseService.idLoginUser}";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection()); 
        return cmd.ExecuteReader();
    }
    
    /// <summary>
    /// Метод UserRegistration
    /// отправляет запрос в БД на регистрацию пользователя
    /// </summary>
    public static void UserRegistration(string login, string pass)
    {
        var querySql = $"INSERT INTO user_app(login, pass) VALUES ('{login.Trim()}', crypt('{pass.Trim()}', gen_salt('bf')))";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection()); 
        cmd.ExecuteNonQuery();
    }
    
    /// <summary>
    /// Метод CheckUserRegistration
    /// отправляет запрос в БД на проверку вводимых авторизационных данных
    /// </summary>
    public static int GetUserRegistration(string login, string pass)
    {
        var querySql = $"SELECT pk FROM user_app WHERE login='{login}' AND pass=crypt('{pass}', pass)";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();

        return reader.Read() ? reader.GetInt32(0) : DatabaseService.NOT_LOGIN;
    }
}