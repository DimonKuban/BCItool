using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BCItester
{
    public partial class Form1 : Form
    {
        List<int> showedQuestions = new List<int>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //loadQuestions();
        }


        

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                // 10 question
            }
            else{
                // quiz
                //var showedQuestions = new List<int>();
                var form = new TrueFalseQuestion();
                form.Show();
                //Random random = new Random();
                //int randomNumber = random.Next(0, data.Count);
                //Question randomQuestion = questions[randomNumber];
                //if (randomQuestion.QType == QuestionType.QType.TrueFalse)
                //{
                //    form.radioButton1.Visible = true;
                //    form.radioButton2.Visible = true;
                //}

            }
        }
    }
}
