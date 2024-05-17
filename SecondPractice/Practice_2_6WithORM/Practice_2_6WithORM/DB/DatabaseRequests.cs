using Microsoft.EntityFrameworkCore;


namespace Practice_2_6WithORM.DB;


public static class DatabaseRequests
{
    public static void AddNote(TodoTask todoTask)
    {
        DatabaseService.GetApplicationContext()
            .TodoTasks.Add(todoTask);
        
        DatabaseService.GetApplicationContext().SaveChanges();
    }
    
    public static void UpdateNote(int pkNote, TodoTask newTodoTask)
    {
        TodoTask todoTask = DatabaseService.GetApplicationContext()
            .TodoTasks
            .Where(task => task.UserPk == DatabaseService.LoggedUser.Pk && task.Pk == pkNote)
            .First();

        todoTask.Title = newTodoTask.Title;
        todoTask.TextTodo = newTodoTask.TextTodo;
        todoTask.TimeEnd = newTodoTask.TimeEnd;
        
        DatabaseService.GetApplicationContext().SaveChanges();
    }
    
    public static void DeleteNote(int pkNote)
    {
        DatabaseService.GetApplicationContext()
            .TodoTasks
            .Where(task => task.UserPk==DatabaseService.LoggedUser.Pk && task.Pk==pkNote)
            .ExecuteDelete();
    }
    
    public static void СlearNote()
    {
        DatabaseService.GetApplicationContext()
            .TodoTasks
            .Where(task => task.UserPk==DatabaseService.LoggedUser.Pk)
            .ExecuteDelete();
    }
    
    public static IQueryable<TodoTask> GetQueryNotes()
    {
        return DatabaseService.GetApplicationContext()
            .TodoTasks
            .Where(task => task.UserPk==DatabaseService.LoggedUser.Pk);
    }
    
    public static IQueryable<TodoTask> GetQueryCompletedNotes()
    {
        return DatabaseService.GetApplicationContext()
            .TodoTasks
            .Where(task => task.UserPk==DatabaseService.LoggedUser.Pk && task.Complited);
    }
    
    public static IQueryable<TodoTask> GetQueryOverdueNotes()
    {
        return DatabaseService.GetApplicationContext()
            .TodoTasks
            .Where(task => task.UserPk==DatabaseService.LoggedUser.Pk && !task.Complited && task.TimeEnd < DateTime.Now);
    }
    
    public static IQueryable<TodoTask> GetQueryInCompletedNotes()
    {
        return DatabaseService.GetApplicationContext()
            .TodoTasks
            .Where(task => task.UserPk==DatabaseService.LoggedUser.Pk && !task.Complited && task.TimeEnd > DateTime.Now);
    }
    
    public static IQueryable<TodoTask> GetQueryNotesOnDateBetweenDates(DateTime start, DateTime end)
    {
        return DatabaseService.GetApplicationContext()
            .TodoTasks
            .Where(task => task.UserPk==DatabaseService.LoggedUser.Pk && 
                           !task.Complited && 
                           task.TimeEnd >= start && task.TimeEnd <= end); //TODO! ADD BETWEEN (ЕГО НЕТУ)
    }

    public static void MarkCompletedNotes(int pkNote)
    {
        TodoTask todoTask = DatabaseService.GetApplicationContext()
            .TodoTasks
            .Where(task => task.UserPk == DatabaseService.LoggedUser.Pk && task.Pk == pkNote)
            .First();
        
        todoTask.Complited = true;
        
        DatabaseService.GetApplicationContext().SaveChanges();
    }

    public static IQueryable<TodoTask> GetNote(int pkNote)
    {
        return DatabaseService.GetApplicationContext()
            .TodoTasks
            .Where(task => task.UserPk == DatabaseService.LoggedUser.Pk && task.Pk == pkNote);
    }
    
    public static void UserRegistration(UserAPP newUser)
    {
        newUser.Pass = _cryptPassword(newUser.Pass);
   
        DatabaseService.GetApplicationContext().Users.Add(newUser);
        DatabaseService.GetApplicationContext().SaveChanges();
    }
    
    public static UserAPP? GetUserAuthorization(UserAPP newUser)
    {
        List<UserAPP> userExists = DatabaseService.GetApplicationContext()
            .Users
            .Where(user => user.Login==newUser.Login)
            .ToList();

        if (userExists.Count != 1) { return null; }
        
        if (!_checkPassword(newUser.Pass, userExists.First().Pass)) { return null; }
        
        return userExists.FirstOrDefault();
    }
    
    private static string _cryptPassword(string pass) 
    {
        // защита от sql иньекций, пробелов в пароле быть не может (правила для ввода)
        pass = pass.Replace(' ', '_');
        
        return DatabaseService.GetApplicationContext()
            .Database.SqlQueryRaw<string>($"SELECT crypt('{pass}', gen_salt('bf'))").ToList().First();
    }
    
    private static bool _checkPassword(string passInput, string passDB)
    {
        // защита от sql иньекций, пробелов в пароле быть не может (правила для ввода)
        passInput = passInput.Replace(' ', '_');
        // защита от sql иньекций, пробелов в хеше быть не может (символ )
        passDB = passDB.Replace(' ', '_');
        
        return DatabaseService.GetApplicationContext()
            .Database.SqlQueryRaw<bool>($"SELECT '{passDB}'=crypt('{passInput}', '{passDB}')").ToList().First();
    }
}