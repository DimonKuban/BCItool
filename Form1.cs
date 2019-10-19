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
        JArray data;
        List<Question> questions = new List<Question>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadQuestions();
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
            if (radioButton1.Checked)
            {
                // 10 question
            }
            else{
                // quiz
                var showedQuestions = new List<int>();
                var form = new TrueFalseQuestion();
                form.Show();
                Random random = new Random();
                int randomNumber = random.Next(0, data.Count);
                Question randomQuestion = questions[randomNumber];
                if (randomQuestion.QType == QuestionType.QType.TrueFalse)
                {
                    form.radioButton1.Visible = true;
                    form.radioButton2.Visible = true;
                }

            }
        }
    }
}
