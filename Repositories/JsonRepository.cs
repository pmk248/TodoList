using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace TodoList.Repositories
{
    internal class JsonRepository : IRepository<TodoModel>
    {
        private string _location = "Data/data.json";
        
        public TodoModel Add(TodoModel todo)
        {
            string file;
            using (StreamReader sr = new StreamReader(_location))
            {
                file = sr.ReadToEnd();
            }
            if (string.IsNullOrEmpty(file))
            {
                throw new Exception("Empty");
            }
            TodoModel[] recieved = JsonConvert.DeserializeObject<TodoModel[]>(file)!;
            List<TodoModel> tdm = new List<TodoModel>();
            for (int i =0; i < recieved.Length; i++)
            {
                tdm.Add(recieved[i]);
            }
            tdm.Add(todo);


            return todo;
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
