
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;


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


            _path = "C:\\Users\\Owner\\Source\\Repos\\TodoList\\Data\\Data.xml";
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
            return todo;
        }

        public void DeleteById(int id)
        {
            foreach (XElement toDo in Doc.Root.Elements().ToList())
            {
                if (id == int.Parse(toDo.Element("Id").Value))
                {
                    toDo.Remove();
                    break;
                }
            }
        }

        public List<TodoModel> GetAll()
        {
            List<TodoModel> toDos = new List<TodoModel>();
            foreach (XElement toDo in Doc.Root.Elements().ToList())
            {
                TodoModel task = new TodoModel();
                task.Title = toDo.Element("Title").Value;
                task.Id = (int.Parse(toDo.Element("Id").Value));
                task.XmlDate = toDo.Element("Date").Value;
                toDos.Add(task);
            }
            return toDos;
        }

        public List<TodoModel> GetAll(Func<TodoModel, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public TodoModel GetById(int id)
        {
            TodoModel task = new TodoModel();
            foreach (XElement toDo in Doc.Root.Elements().ToList())
            {
                if (id == int.Parse(toDo.Element("Id").Value))
                {
                    task.Title = toDo.Element("Title").Value;
                    task.Id = (int.Parse(toDo.Element("Id").Value));
                    task.XmlDate = toDo.Element("Date").Value;
                    break;
                }
            }
            return task;
        }

        public TodoModel Update(TodoModel todo)
        {
            foreach (XElement XMLtoDo in Doc.Root.Elements().ToList())
            {
                if (todo.Id == int.Parse(XMLtoDo.Element("Id").Value))
                {
                    XMLtoDo.Element("Title").Value = todo.Title;
                    XMLtoDo.Element("Id").Value = todo.Id.ToString();
                    XMLtoDo.Element("Date").Value = todo.XmlDate;
                    break;
                }

            }
            return todo;
        }
    }
}
