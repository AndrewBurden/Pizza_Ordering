using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;


namespace StockManagement
{
    public partial class Form1 : Form
    {
        ArrayList itemList = new ArrayList();

        public Form1()
        {
            InitializeComponent();
            panel1.Parent = this;
            panel2.Parent = this;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //listBox1.Items.Add("Cheese    16 Packs    08/15/2016");
            //listBox1.Items.Add("Flour      2 Bags     09/10/2016");       
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel1.Visible = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
            panel2.BringToFront();

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel2.BringToFront();
            int selectedIndex = listBox1.SelectedIndex;
            Item selectedItem = (Item)itemList[selectedIndex];
            textBox1.Text = selectedItem.getName();
            textBox2.Text = selectedItem.getUnitName();
            textBox3.Text = selectedItem.getCurrentLevel().ToString();
            textBox4.Text = selectedItem.getLowLevel().ToString();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "0";
            textBox4.Text = "";
            panel1.Visible = false;
            panel2.Visible = true;
            panel2.BringToFront();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel1.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int errorType = 0;
            Item newItem = new Item();
            errorType = newItem.validateInput(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
            if (errorType == 1)
                MessageBox.Show("The quality should be a Positive number or Zero.");
            else if (errorType == 2)
                MessageBox.Show("The minimum level should be a Positive number or Zero.");
            else if (errorType == 3)
                MessageBox.Show("The item is aready in the database.");
            else
            {
                listBox1.Items.Add(textBox1.Text + " has been added to the database.");
                MessageBox.Show("The new item " + textBox1.Text + " has been added to the database successfully.");
                button6_Click(sender, e);
            }
            newItem = null;
            GC.Collect();
            button1_Click_2(sender, e);
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            panel2.Visible = false;
            itemList.Clear();
            itemList = Item.getItemList();
            listBox1.Items.Clear();
            foreach (Item nextItem in itemList)
            {
                string itemName = nextItem.getName();
                listBox1.Items.Add(itemName);
            }
            
        }
    }
}
