using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sorting
{
    public partial class Form1 : Form
    {
        private List<Book> books = new List<Book>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            setComboBoxSort();
            fetchBooks();
            renderBooks();
        }

        private void setComboBoxSort()
        {
            using (Model1 db = new Model1())
            {
                foreach (Sorting sort in db.Sorting)
                {
                    comboBox1.Items.Add(sort.sort);
                }
            }
        }

        public void fetchBooks()
        {
            using (Model1 db = new Model1()) 
            {
                this.books.Clear();
                foreach (Book book in db.Books)
                {
                    this.books.Add(book);
                }
            }
        }

        public void renderBooks()
        { 
            container.Controls.Clear();

            Label labelTitle = new Label();
            labelTitle.Location = new Point(20, 10);
            labelTitle.Text = "Название книги";
            labelTitle.Width = 160;
            labelTitle.Height = 20;
            container.Controls.Add(labelTitle);

            Label labelPages = new Label();
            labelPages.Location = new Point(210, 10);
            labelPages.Text = "Страницы";
            labelPages.Width = 160;
            labelPages.Height = 20;
            container.Controls.Add(labelPages);

            Label labelCost = new Label();
            labelCost.Location = new Point(410, 10);
            labelCost.Text = "Цена";
            labelCost.Width = 160;
            labelCost.Height = 20;
            container.Controls.Add(labelCost);

            int y = 0;

            foreach (Book book in this.books)
            {
                GroupBox box = new GroupBox();
                box.Location = new Point(10, 40 + y++ * 40);
                box.Width = 690;
                box.Height = 40;
                box.FlatStyle = FlatStyle.Flat;

                Label title = new Label();
                title.Text = $"{ book.title }";
                title.Width = 150;
                title.Location = new Point(10, 14);


                Label pages = new Label();
                pages.Text = $"{book.pages}";
                pages.Width = 200;
                pages.Location = new Point(200, 14);

                Label cost = new Label();
                cost.Text = $"{(int) book.cost} руб.";
                cost.Width = 100;
                cost.Location = new Point(400, 14);

                Button btnEdit = new Button();
                btnEdit.Text = "Изменить";
                btnEdit.Location = new Point(500, 12);
                btnEdit.Click += (s, e) => BtnEdit_Click(book);

                Button btnDelete = new Button();
                btnDelete.Text = "Удалить";
                btnDelete.Location = new Point(590, 12);
                btnDelete.Click += (s, e) => BtnDelete_Click(book);


                box.Controls.Add(title);
                box.Controls.Add(pages);
                box.Controls.Add(cost);
                box.Controls.Add(btnEdit);
                box.Controls.Add(btnDelete);

                container.Controls.Add(box);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    {
                        books = this.books.OrderByDescending(book => book.cost).ToList();
                        this.renderBooks();
                        break;
                    }
                case 1:
                    {
                        books = this.books.OrderBy(book => book.cost).ToList();
                        this.renderBooks();
                        break;
                    }

            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.fetchBooks();
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    {
                        this.renderBooks();
                        break;
                    }
                case 1:
                    {
                        this.books = books.Where(book => book.pages >= 100).ToList();
                        this.renderBooks();
                        break;
                    }
                case 2:
                    {
                        this.books = books.Where(book => book.pages >= 150).ToList();
                        this.renderBooks();
                        break;
                    }
                case 3:
                    {
                        this.books = books.Where(book => book.pages >= 200).ToList();
                        this.renderBooks();
                        break;
                    }

            }
            comboBox1.Text = "Сортировка по цене";
        }

        private void BtnEdit_Click(Book book)
        {
            Form2 editForm = new Form2();

            editForm.method = "update";
            editForm.id = book.id;
            editForm.title = book.title;
            editForm.cost = book.cost;
            editForm.pages = book.pages;
            editForm.mainForm = this;

            this.Hide();
            editForm.Show();
            editForm.Focus();
        }

        private void BtnDelete_Click(Book book)
        {
            DialogResult result = MessageBox.Show($"Вы уверены, что хотите удалить книгу {book.title}?", "Удалить", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                using (Model1 db = new Model1())
                {
                    db.Books.Remove(db.Books.FirstOrDefault(el => el.id == book.id));
                    db.SaveChanges();

                    this.fetchBooks();
                    this.renderBooks();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 editForm = new Form2();

            editForm.method = "create";
            editForm.mainForm = this;

            this.Hide();
            editForm.Show();
            editForm.Focus();
        }
    }
}
