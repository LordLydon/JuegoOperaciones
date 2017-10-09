using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JuegoOperaciones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage
    {
        private int totalGames;
        private int currGame;
        private Random Generator;
        private PageModel Model;
        
        public int CorrectCount { get; private set; }
        public event EventHandler Finished;

        public GamePage(int games = 3)
        {
            totalGames = games;
            currGame = 1;
            CorrectCount = 0;
            SetTitle();
            Generator = new Random();

            InitializeComponent();

            Model = RandomValues();
            BindingContext = Model;
        }

        private PageModel RandomValues()
        {
            int op1 = Generator.Next(0, 100);
            int op2 = Generator.Next(0, 100);
            int sign = Generator.Next(0, 2);
            int result;
            if (sign == 1)
            {
                // SUM
                result = op1 + op2;
            }
            else
            {
                //SUB
                result = op1 - op2;
            }

            return new PageModel
            {
                NextBtnText = currGame == totalGames ? "Finalizar" : "Continuar",
                Op1 = op1,
                Op2 = op2,
                Sign = sign == 0 ? "-" : "+",
                Result = result
            };
            ;
        }

        private void Next(object sender, EventArgs e)
        {
            //Process sent answer
            int ans;
            if (int.TryParse(AnswerEntry.Text, out ans))
            {
                //Check if answer is correct
                if (ans == Model.Result)
                    CorrectCount++;
                //Clear Entry
                AnswerEntry.Text = "";
            }
            currGame++;

            //Prepare next Question or Return to MainPage
            if (currGame < totalGames + 1)
            {
                //Next Question
                Model = RandomValues();
                BindingContext = Model;
                SetTitle();
            }
            else
            {
                //Return to MainPage
                Finished(this, EventArgs.Empty);
                Navigation.PopAsync(true);
            }
        }

        private void SetTitle()
        {
            Title = $"#{currGame} de {totalGames} Intentos";
        }

        private class PageModel
        {
            public int Op1 { get; set; }
            public string Sign { get; set; }
            public int Op2 { get; set; }
            public string NextBtnText { get; set; }
            public int Result { get; set; }
        }
    }
}