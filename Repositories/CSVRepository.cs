using Microsoft.VisualBasic.FileIO;
using System.Globalization;

namespace TodoList.Repositories
{
    internal class CSVRepository : IRepository<TodoModel>
    {

        string path;
        List<TodoModel> todoList = [];

        public CSVRepository()
        {
            path = BuildPath(["Data", "Data.csv"]);
            todoList = GetAll();
        }

        private string BuildPath(params string[] xmlPath)
        {
            string[] fullPath = new string[xmlPath.Length + 1];
            fullPath[0] = AppDomain.CurrentDomain.BaseDirectory;
            Array.Copy(xmlPath, 0, fullPath, 1, xmlPath.Length);
            return Path.Combine(fullPath);
        }

        public TodoModel Add(TodoModel todo)
        {
            todoList.Add(todo);
            WriteToFile();
            return todo;
        }

        public void DeleteById(int id)
        {
            int index = todoList.FindIndex(t => t.Id == id);
            if (index != -1) todoList.RemoveAt(index);
            WriteToFile();
        }

        public List<TodoModel> GetAll()
        {
            List<TodoModel> temp = [];
            using (TextFieldParser parser = new TextFieldParser(path))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                parser.ReadFields();
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    var dateTime = DateTime.ParseExact(fields[2], "dd/mm/yyyy", CultureInfo.InvariantCulture);
                    var dateOnly = DateOnly.FromDateTime(dateTime);
                    TodoModel model = new TodoModel(int.Parse(fields[0]), fields[1], dateOnly);
                    temp.Add(model);
                }
            }
            return temp;
        }

        public List<TodoModel> GetAll(Func<TodoModel, bool> predicate)
        {
            return todoList.Where(t => predicate(t)).ToList();
        }

        public TodoModel? GetById(int id) => todoList.Find(t => t.Id == id);


        public TodoModel Update(TodoModel todo)
        {
            int index = todoList.FindIndex(t => t.Id == todo.Id);
            if (index != -1)
            {
                todoList[index] = todo;
            }
            WriteToFile();
            return todo;
        }

        public int getNextId() => todoList.Count > 0 ? todoList.Max(t => t.Id) + 1 : 1;

        private void WriteToFile()
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                string clientHeader = $"id,title,date" + Environment.NewLine;
                writer.WriteLine(clientHeader);
                foreach (var item in todoList)
                {
                    writer.WriteLine($"{item.Id},{item.Title}, {item.Date.ToString()}");
                }
            }
        }
    }
}
