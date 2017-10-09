using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JuegoOperaciones
{
	public partial class MainPage : ContentPage
	{
		private string name;
		
		public MainPage()
		{
			Title = "Juego de Operaciones";
			InitializeComponent();

			name = "";
		}

		private async void Start(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(UserEntry.Text) && CountsPicker.SelectedIndex != -1)
			{
				name = UserEntry.Text;
				var count = (int) CountsPicker.SelectedItem;

				GamePage gamePage = new GamePage(count);
				gamePage.Finished += (o, args) => DisplayAlert($"Terminaste {name}!",
					$"Obtuviste {gamePage.CorrectCount} aciertos de {count} intentos!", "OK");

				await Navigation.PushAsync(gamePage);
			}
			else
			{
				await DisplayAlert("Error!", "Por favor ingrese valores en todos los campos para continuar!", "OK");
			}
		}
	}
}
