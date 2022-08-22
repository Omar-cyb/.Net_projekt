using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektowanieNET
{
    public partial class AddForm : Form
    {
        ContactBook k = null!;

        public AddForm()
        {
            InitializeComponent();
        }

        private Contact Stan
        {
            get => new Contact
            {
                FirstName = textBox1.Text,
                LastName = textBox2.Text,
                Email = textBox4.Text,
                Phone = textBox3.Text,
                Birthday = dateTimePicker1.Value
            };
        }

        private void Odswiez()
        {
            if (Contact.Validate(Stan))
                button2.Enabled = true;
            else
                button2.Enabled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e) => Odswiez();
        private void textBox2_TextChanged(object sender, EventArgs e) => Odswiez();
        private void textBox3_TextChanged(object sender, EventArgs e) => Odswiez();
        private void textBox4_TextChanged(object sender, EventArgs e) => Odswiez();
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) => Odswiez();

        private void button2_Click(object sender, EventArgs e)
        {
            k.Add(Stan);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddForm_Shown(object sender, EventArgs e)
        {
            k = ContactBook.Book;
        }
    }
}
