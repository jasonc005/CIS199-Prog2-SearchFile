using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace HA2_1
{
    public partial class searchDBForm : Form
    {
        
        public searchDBForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            resultListbox.Items.Clear();

            string name = "";
            int age = 0;
            string gender = "";

            if (nameTextbox.Text != "")
            {
                name = nameTextbox.Text;

                if (ageTextbox.Text != "")
                {
                    if (int.TryParse(ageTextbox.Text, out age))
                    {
                        if (age > 0)
                        {
                            age = int.Parse(ageTextbox.Text);
                        }
                        else
                        {
                            MessageBox.Show("Provide a valid positive integer as age. Age is not being considered for search.");
                        }
                    }
                }

                if (genderTextbox.Text != "")
                {
                    if (genderTextbox.Text.ToUpper() == "M" || genderTextbox.Text.ToUpper() == "F")
                    {
                        gender = genderTextbox.Text.ToUpper();
                    }
                    else
                    {
                        MessageBox.Show("Provide a valid gender (m,M,f,F). Gender is not being considered in the search.");
                    }
                }

                StreamReader dbInput = File.OpenText("LeaderData.txt");

                while (!dbInput.EndOfStream)
                {
                    string line = dbInput.ReadLine();
                    string tempName = "";
                    int tempAge = 0;
                    string tempGender = "";

                    getDataFromLine(line, ref tempName, ref tempAge, ref tempGender);

                    if (tempName.ToUpper().IndexOf(name.ToUpper()) != -1)
                    {
                        if(gender != "" && age > 0 && gender == tempGender && age == tempAge)
                        {
                            resultListbox.Items.Add("Name=" + tempName + ", Age=" + tempAge + ", Gender=" + tempGender + "\n");
                        }
                        else if (gender != "" && age == 0 && gender == tempGender)
                        {
                            resultListbox.Items.Add("Name=" + tempName + ", Age=" + tempAge + ", Gender=" + tempGender + "\n");
                        }
                        else if (gender == "" && age > 0 && age == tempAge)
                        {
                            resultListbox.Items.Add("Name=" + tempName + ", Age=" + tempAge + ", Gender=" + tempGender + "\n");
                        }
                        else if (gender == "" && age == 0)
                        {
                            resultListbox.Items.Add("Name=" + tempName + ", Age=" + tempAge + ", Gender=" + tempGender + "\n");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Name is necessary to initiate the search.");
            }
        }

        //Here goes the method to set the name, age and gender from the line.
        void getDataFromLine(string line, ref string name, ref int age, ref string gender)
        {
            int nameComma = line.IndexOf(",");
            int ageComma = line.IndexOf(",", nameComma + 1);
            name = line.Substring(0, nameComma);
            age = int.Parse(line.Substring(nameComma + 1, ageComma - nameComma - 1));
            gender = line.Substring(ageComma + 1);
        }
    }
}
