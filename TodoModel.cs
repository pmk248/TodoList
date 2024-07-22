using System.Xml.Serialization;

namespace TodoList
{
    [XmlRoot("Todo")] 
    public class TodoModel
    {
        public TodoModel(string title, DateOnly date)
        {
            Id = counter++;
            Title = title;
            Date = date;
        }

        static private int counter = 1; 
        
        [XmlElement("ID")] 
        public int Id { get; set; }

        [XmlElement("Title")] 
        public string Title { get; set; }

        //[XmlElement("Date")] 
        //public string XmlDate
        //{
        //    get { return Date.ToString("yyyy-MM-dd"); } 
        //    set { Date = DateOnly.Parse(value); } 
        //}

        [XmlIgnore] 
        public DateOnly Date { get; set; }
    }
}
