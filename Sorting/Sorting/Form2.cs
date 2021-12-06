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
    public partial class Form2 : Form
    {
        public Form1 mainForm;

        public string method { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public int pages { get; set; }
        public decimal cost { get; set; }


        public Form2()
        {
            InitializeComponent();
        }


        private void Form2_Shown(object sender, EventArgs e)
        {
            labelText.Text = title;
            pagesText.Text = pages.ToString();
            costText.Text = cost.ToString();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (method == "create") createBook();
            else if (method == "update") updateBook();

        }

        private void createBook()
        {
            using (Model1 db = new Model1())
            {
                if (ValidInputs())
                {
                    Book book = new Book();

                    book.title = labelText.Text;
                    book.pages = Convert.ToInt32(pagesText.Text);
                    book.cost = Convert.ToDecimal(costText.Text);

                    db.Books.Add(book);
                    db.SaveChanges();
                    mainForm.fetchBooks();
                    mainForm.renderBooks();
                    this.Close();
                }
            }
        }

        private void updateBook()
        {
            using (Model1 db = new Model1())
            {
                Book book = db.Books.SingleOrDefault(el => el.id == this.id);

                if (ValidInputs())
                {
                    book.title = labelText.Text;
                    book.pages = Convert.ToInt32(pagesText.Text);
                    book.cost = Convert.ToDecimal(costText.Text);

                    db.SaveChanges();
                    mainForm.fetchBooks();
                    mainForm.renderBooks();
                    this.Close();
                }
            }
        }

        private bool ValidInputs()
        {
            if (ValidTtilte() && ValidPages() && ValidCost()) return true;
            return false;
        }

        private bool ValidTtilte()
        {
            if (labelText.Text == "")
            {
                MessageBox.Show("Название книги не может быть пустым");
                return false;
            }

            return true;
        }

        private bool ValidPages()
        {
            if (pagesText.Text == "")
            {
                MessageBox.Show("Поле страницы не может быть пустым");
                return false;
            }


            return true;
        }

        private bool ValidCost()
        {
            int num;
            decimal dec;
            if (costText.Text == "")
            {
                MessageBox.Show("Стоимость книги не может быть пустой");
                return false;
            } else if (!decimal.TryParse(costText.Text, out dec) && !int.TryParse(costText.Text, out num))
            {
                MessageBox.Show("Стоимость книги далжно быть числом");
                return false;
            }


            return true;
        }
    }
}
