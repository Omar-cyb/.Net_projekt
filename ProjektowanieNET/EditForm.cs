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
    public partial class EditForm : Form
    {
        ContactBook k = null!;

        public EditForm()
        {
            InitializeComponent();
        }

        public Contact Docelowy { get; set; } = null!;

        private void EditForm_Shown(object sender, EventArgs e)
        {
            k = ContactBook.Book;
            if (Docelowy is null)
                throw new Exception("Nie przekazano docelowego kontaktu");

            if (Docelowy.Id is null)
                throw new Exception("Docelowy kontakt nie miesci id");

            Stan = Docelowy;
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
            set
            {
                textBox1.Text = value.FirstName;
                textBox2.Text = value.LastName;
                textBox4.Text = value.Email;
                textBox3.Text = value.Phone;
                dateTimePicker1.Value = value.Birthday;
            }
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
            k.Edit((int) Docelowy.Id!, Stan);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
