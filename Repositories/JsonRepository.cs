using Newtonsoft.Json;

namespace TodoList.Repositories
{
    internal class JsonRepository : IRepository<TodoModel>
    {
        private string _location = "Data/Data.json";
        private List<TodoModel> tdm;

        public JsonRepository()
        {
            tdm = GetAllAsList();
        }

        public TodoModel Add(TodoModel todo)
        {
            tdm.Add(todo);
            Save(tdm);
            return todo;
        }

        public void DeleteById(int id)
        {
            foreach (var item in tdm)
            {
                if (item.Id == id)
                {
                    tdm.Remove(item);
                }
            }
            Save(tdm);
        }

        public List<TodoModel> GetAll()
        {
            return tdm;
        }

        public List<TodoModel> GetAll(Func<TodoModel, bool> predicate)
        {
            return tdm.Where(predicate).ToList();
        }

        public TodoModel GetById(int id)
        {
            TodoModel todo = null;
            foreach (TodoModel item in tdm)
            {
                if (item.Id == id)
                {
                    todo = item;
                    break;
                }
            }
            return todo;
        }

        public TodoModel Update(TodoModel todo)
        {
            for (int i = 0; i < tdm.Count; i++)
            {
                if (tdm[i].Id == todo.Id)
                {
                    tdm[i] = todo;
                    break;
                }
            }
            Save(tdm);
            return todo;
        }


        private void Save(List<TodoModel> todo)
        {
            string json = JsonConvert.SerializeObject(todo);
            using (StreamWriter sw = new StreamWriter(_location))
            {
                sw.Write(json);
            }
        }

        private List<TodoModel> GetAllAsList()
        {
            string file;
            using (StreamReader sr = new StreamReader(_location))
            {
                file = sr.ReadToEnd();
            }
            if (string.IsNullOrEmpty(file))
            {
                List<TodoModel> tdmod = new List<TodoModel>();
                return tdmod;
            }

            TodoModel[] recieved = JsonConvert.DeserializeObject<TodoModel[]>(file);
            List<TodoModel> tdm = new List<TodoModel>();
            for (int i = 0; i < recieved.Length; i++)
            {
                tdm.Add(recieved[i]);
            }
            return tdm;
        }

        public int getNextId() => tdm.Count > 0 ? tdm.Max(x => x.Id) + 1 : 1;
    }
}
