
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace TodoList.Repositories
{
    internal class XMLRepository : IRepository<TodoModel>
    {
        private string _path;
        public XDocument Doc;
        public XElement Root;
        private List<TodoModel> todoList = [];

        public XMLRepository()
        {


            _path = "data\\Data.xml";
            Doc = XDocument.Load(_path);
            Root = Doc.Root ?? throw new Exception("no root element found!");
            todoList = GetAll();
        }
        public TodoModel Add(TodoModel todo)
        {
            XElement XMLtoDo = new XElement("Todo",
              new XElement("Title", todo.Title),
              new XElement("Id", todo.Id),
              new XElement("Date", todo.Date)
              );
            Doc.Root.Add(XMLtoDo);
            Doc.Save(_path);
            todoList = GetAll();
            return todo;
        }

        public void DeleteById(int id)
        {
            foreach (XElement toDo in Doc.Root.Elements().ToList())
            {
                if (id == int.Parse(toDo.Element("Id").Value))
                {
                    toDo.Remove();
                    todoList = GetAll();
                    return;
                }
            }
        }

        public List<TodoModel> GetAll()
        {
            List<TodoModel> toDos = [];
            foreach (XElement toDo in Root.Elements().ToList())
            {
                string title = toDo.Element("Title").Value;
                int id = (int.Parse(toDo.Element("Id").Value));
                DateOnly date = DateOnly.Parse(toDo.Element("Date").Value);
                TodoModel task = new TodoModel(id, title, date );
                toDos.Add(task);
            }
            return toDos;
        }

        public List<TodoModel> GetAll(Func<TodoModel, bool> predicate) => todoList.Where(predicate).ToList();
        

        public TodoModel? GetById(int id) => todoList.Find(t => t.Id == id);

        public TodoModel Update(TodoModel todo)
        {
            int index = todoList.FindIndex(t => t.Id == todo.Id);
            if (index != -1) {
                todoList[index] = todo;
            }
            foreach (XElement XMLtoDo in Root.Elements().ToList())
            {
                if (todo.Id == int.Parse(XMLtoDo.Element("Id").Value))
                {
                    XMLtoDo.Element("Title").Value = todo.Title;
                    XMLtoDo.Element("Id").Value = todo.Id.ToString();
                    XMLtoDo.Element("Date").Value = todo.Date.ToString();
                    break;
                }
            }
            todoList = GetAll();
            return todo;
        }

        public int getNextId() =>  todoList.Count == 0 ? 1 : todoList.Max(t => t.Id) + 1;
    }
}
