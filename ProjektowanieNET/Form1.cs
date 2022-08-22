using System.Text.Json;

namespace ProjektowanieNET;

public partial class Form1 : Form
{
    ContactBook k = null!;

    public Form1()
    {
        InitializeComponent();
    }

    private string[] Lista(IEnumerable<Contact> l)
    {
        return l.Select(k => k.ToString()).ToArray();
    }


    private void Form1_Shown(object sender, EventArgs e)
    {
        k = ContactBook.Book;
        Odswiez();
        OdswiezPrzyciski();
    }

    private void Odswiez()
    {
        listBox1.DataSource = Lista(k.Contacts);
        listBox2.DataSource = Lista(k.Birthdays);
    }

    private void OdswiezWyszukaj(string s)
    {
        listBox1.DataSource = Lista(k.Search(s));
        listBox2.DataSource = Lista(k.SearchBirthday(s));
    }

    private void OdswiezPrzyciski()
    {
        if (listBox1.SelectedItems.Count == 0 && listBox2.SelectedItems.Count == 0)
        {
            button2.Enabled = false;
            button3.Enabled = false;
        }
        else
        {
            button2.Enabled = true;
            button3.Enabled = true;
        }
    }

    private Contact PierwszyWybrany()
    {
        if (listBox1.SelectedItems.Count > 0)
            return k.Contacts.Where(k => k.MatchesExactly(listBox1.SelectedItems[0].ToString())).First();
        else if (listBox2.SelectedItems.Count > 0)
            return k.Contacts.Where(k => k.MatchesExactly(listBox2.SelectedItems[0].ToString())).First();

        throw new Exception("Wystapil blad");
    }

    private void button4_Click(object sender, EventArgs e)
    {
        OdswiezWyszukaj(textBox1.Text);
    }

    private void EditClosed(object sender, EventArgs e)
    {
        Odswiez();
        OdswiezPrzyciski();
    }
    private void AddClosed(object sender, EventArgs e)
    {
        Odswiez();
        OdswiezPrzyciski();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        var add = new AddForm();
        add.FormClosed += AddClosed;
        add.Show();
    }

    private void button2_Click(object sender, EventArgs e)
    {
        var edit = new EditForm() { Docelowy = PierwszyWybrany() };
        edit.FormClosed += EditClosed;
        edit.Show();
    }

    private void button3_Click(object sender, EventArgs e)
    {
        k.Remove(PierwszyWybrany());
        Odswiez();
        OdswiezPrzyciski();
    }
}
