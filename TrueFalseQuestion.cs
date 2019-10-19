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
    public partial class TrueFalseQuestion : Form
    {
        JArray data;
        List<Question> questions = new List<Question>();
        List<int> showedQUestions = new List<int>();
        Question question;
        public TrueFalseQuestion()
        {
            InitializeComponent();
        }

        private void TrueFalseQuestion_Load(object sender, EventArgs e)
        {
            loadQuestions();

            var showedQuestions = new List<int>();
            //var form = new TrueFalseQuestion();
            //form.Show();
            Random random = new Random();
            //int randomNumber = random.Next(0, data.Count);
            //Question randomQuestion = questions[randomNumber];
            //if (randomQuestion.QType == QuestionType.QType.TrueFalse)
            //{
            //    this.radioButton1.Visible = true;
            //    this.radioButton2.Visible = true;
            //}
        }

        public void loadQuestions()
        {

            // read JSON directly from a file
            using (StreamReader file = File.OpenText(@"questions.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                data = (JArray)JToken.ReadFrom(reader);
            }

            this.label1.Text = "There are " + data.Count + " questions available";

            int counter = 0;
            questions = new List<Question>();

            foreach (var item in data.Children())
            {
                var itemProperties = item.Children<JProperty>();
                //int index = (int)itemProperties.FirstOrDefault(x => x.Name == "_id").Value;
                string qt = itemProperties.FirstOrDefault(x => x.Name == "QType").Value.ToString();
                QuestionType.QType qType = (QuestionType.QType)Enum.Parse(typeof(QuestionType.QType), qt, true);
                string CorrectAnswer = itemProperties.FirstOrDefault(x => x.Name == "CorrectAnswer").Value.ToString();
                string QuestionText = itemProperties.FirstOrDefault(x => x.Name == "QuestionText").Value.ToString();
                questions.Add(new Question(qType, QuestionText, CorrectAnswer, counter++));
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            showedQUestions.Add(this.question.index);
            this.label2.Visible = true;
            this.textBox1.Visible = true;
            
            if(this.question.QType == QuestionType.QType.TrueFalse)
            {
                if(this.radioButton1.Checked == true && this.question.CorrectAnswer == "True")
                {
                    this.textBox1.Text = "Correct";
                    this.textBox1.ForeColor = Color.FromArgb(0, 255, 0);
                }
                else if(this.radioButton1.Checked == false && this.question.CorrectAnswer == "False")
                {
                    this.textBox1.Text = "Correct";
                    this.textBox1.ForeColor = Color.FromArgb(0, 255, 0);
                }
                else
                {
                    this.textBox1.Text = "Incorrect";
                    this.textBox1.ForeColor = Color.FromArgb(255, 0, 0);
                }
                
            } else if(this.question.QType == QuestionType.QType.MissingWord || this.question.QType == QuestionType.QType.Multiple)
            {
                if(this.question.CorrectAnswer.ToUpper() == this.textBox2.Text.Trim().ToUpper())
                {
                    this.textBox1.Text = "Correct";
                    this.textBox1.ForeColor = Color.FromArgb(0, 255, 0);
                }
                else
                {
                    this.textBox1.Text = this.question.CorrectAnswer;
                    this.textBox1.ForeColor = Color.FromArgb(255, 0, 0);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //loadQuestions();
            clearControl();

            if (this.questions.Count == 0)
            {
                this.textBox1.Visible = true;
                this.textBox1.Text = "No more questions available";
                this.textBox1.ForeColor = Color.FromArgb(0, 0, 255);
            }
            else
            {
                
                Random random = new Random();
                int randomNumber = random.Next(0, questions.Count);
                question = questions[randomNumber];
                questions.RemoveAt(randomNumber);



                if (question.QType == QuestionType.QType.TrueFalse)
                {
                    this.radioButton1.Visible = true;
                    this.radioButton2.Visible = true;
                } else if(question.QType == QuestionType.QType.MissingWord || question.QType == QuestionType.QType.Multiple)
                {
                    this.textBox2.Visible = true;
                    this.label3.Visible = true;
                }

                this.richTextBox1.Text = question.QuestionText;
            }
        }

        public void clearControl()
        {
            this.radioButton1.Visible = false;
            this.radioButton2.Visible = false;
            this.textBox1.Visible = false;
            this.textBox2.Visible = false;
            this.label2.Visible = false;
            this.label3.Visible = false;
            this.label1.Visible = false;
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            
        }
    }
}
