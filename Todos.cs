using ReaLTaiizor.Forms;

namespace TodoList
{
    enum Mode
    {
        Add,
        Edit
    }
    internal partial class Todos : MaterialForm
    {
        private List<TodoModel> todos;
        private Mode mode;
        private IRepository<TodoModel> repository;

        public Todos(IRepository<TodoModel> repository)
        {
            InitializeComponent();
            todos = new List<TodoModel>();
            dataGridView_tasks.DataSource = todos;
            this.repository = repository;
        }

        private void populateViewWithTodo()
        {
            todos.Clear();
            foreach (var todo in repository.GetAll())
            {
                todos.Add(todo);
            }
        }

        // populate form from selected row
        private void dataGridView_tasks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex <= 0)
            {
                var selectedTodo = todos[e.RowIndex];
                textbox_title.Text = selectedTodo.Title;
                hopeDatePicker1.Date = selectedTodo.Date.ToDateTime(TimeOnly.MinValue);
                SetMode(mode);
            }
        }

        private void SetMode(Mode mode)
        {
            this.mode = mode;
            button_action.Text = mode == Mode.Add ? "Add" : "Edit";
        }

        // add or edit based on mode
        private void button_action_Click(object sender, EventArgs e)
        {
            if (mode == Mode.Add)
            {
                DateTime dt = hopeDatePicker1.Date;
                DateOnly d = new DateOnly(dt.Year, dt.Month, dt.Day);
                var newTodo = new TodoModel(textbox_title.Text, d);
            }
            else if (mode == Mode.Edit)
            {
                var selectedTodo = todos[dataGridView_tasks.CurrentRow.Index];
                selectedTodo.Title = textbox_title.Text;
                selectedTodo.Date = DateOnly.FromDateTime(hopeDatePicker1.Date);
                repository.Update(selectedTodo);
            }
            populateViewWithTodo();
            SetMode(Mode.Add);
            ClearForm();
        }
        private void ClearForm()
        {
            textbox_title.Clear();
            hopeDatePicker1.Date = DateTime.Today;
        }
    }
}
