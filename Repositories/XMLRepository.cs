
using System.Xml.Linq;


namespace TodoList.Repositories
{
    internal class XMLRepository : IRepository<TodoModel>
    {
        private string _path;
        public XDocument Doc;
        public XElement Root;
        public XMLRepository(string path)
        {
            _path = path;
            Doc = XDocument.Load(_path);
            Root = Doc.Root ?? throw new Exception("no root element found!");
        }
        public TodoModel Add(TodoModel todo)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public List<TodoModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<TodoModel> GetAll(Func<TodoModel, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public TodoModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public TodoModel Update(TodoModel todo)
        {
            throw new NotImplementedException();
        }
    }
}
