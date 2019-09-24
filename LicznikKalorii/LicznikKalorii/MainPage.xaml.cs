using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using SQLite;
using System.ComponentModel;
using SkiaSharp;
using Entry = Microcharts.Entry;
using Microcharts;

namespace LicznikKalorii
{
    [DesignTimeVisible(true)]
    public partial class MainPage : TabbedPage
    {
        SQLiteConnection database;
        private string databasePath;
        private int mode;

        
        
       

        public MainPage()
        {
            InitializeComponent();

            databasePath = DependencyService.Get<ISaveAndLoad>().GetLocalFilePath("BazaSQLite.db3");

            database = new SQLiteConnection(databasePath);
            database.CreateTable<TabKcal>();



        }


        private void ButZapisz_Clicked(object sender, EventArgs e)
        {
           

            TabKcal t1 = new TabKcal();
            t1.Zapotrzebowanie = Convert.ToInt32(eLimitKal.Text);
            t1.Sniadanie = Convert.ToInt32(eSniadanie.Text);
            t1.IISniadanie = Convert.ToInt32(eIISniadanie.Text);
            t1.Obiad = Convert.ToInt32(eObiad.Text);
            t1.Podwieczorek = Convert.ToInt32(ePodwieczorek.Text);
            t1.Kolacja = Convert.ToInt32(eKolacja.Text);

            if (t1.Zapotrzebowanie < 1000)
            {
                DisplayAlert("Błąd", "Zapotrzebowanie musi być większe niż 1000", "Cancel");
            }
            if ((t1.Sniadanie | t1.IISniadanie | t1.Obiad | t1.Podwieczorek | t1.Kolacja) < 0)
            {
                DisplayAlert("Błąd", "Wartości nie mogą być ujemne", "Cancel");
            }

            int sniadanie;
            int IIsniadanie;
            int obiad;
            int podwieczorek;
            int kolacja;
            List<Entry> entries = new List<Entry>

            {

                new Entry(sniadanie = Convert.ToInt32(eSniadanie.Text))
                {

                    Color = SKColor.Parse("#FF1493"),
                    Label = "Sniadanie",
                    ValueLabel = Convert.ToString(sniadanie)
                },
                new Entry(IIsniadanie = Convert.ToInt32(eIISniadanie.Text))
                {
                    Color = SKColor.Parse("#00BFFF"),
                    Label = "II Sniadanie",
                    ValueLabel = Convert.ToString(IIsniadanie)
                },
                new Entry(obiad = Convert.ToInt32(eObiad.Text))
                {
                    Color = SKColor.Parse("#00CED1"),
                    Label = "Obiad",
                    ValueLabel = Convert.ToString(obiad)
                },
                new Entry(podwieczorek = Convert.ToInt32(ePodwieczorek.Text))
                {
                    Color = SKColor.Parse("#3C17C4"),
                    Label = "Podwieczorek",
                    ValueLabel = Convert.ToString(podwieczorek)
                },
                new Entry(kolacja = Convert.ToInt32(eKolacja.Text))
                {
                    Color = SKColor.Parse("#6EFF14"),
                    Label = "Kolacja",
                    ValueLabel = Convert.ToString(kolacja)
                }

            };

            Chart1.Chart = new BarChart { Entries = entries, LabelTextSize = 25  };

            var s1 = database.Insert(t1);

        }

        private void WczytajDane_Clicked(object sender, EventArgs e)
        {
            var dane = database.Table<TabKcal>();

            //int i = 0;

            foreach (var wiersz in dane)
            {
                LabelZapotrzebowanie.Text = wiersz.Zapotrzebowanie.ToString();
                LabelSniadanie.Text = wiersz.Sniadanie.ToString();
                LabelIISniadanie.Text = wiersz.IISniadanie.ToString();
                LabelObiad.Text = wiersz.Obiad.ToString();
                LabelPodwieczorek.Text = wiersz.Podwieczorek.ToString();
                LabelKolacja.Text = wiersz.Kolacja.ToString();

                LabelSuma.Text = (wiersz.Sniadanie + wiersz.IISniadanie + wiersz.Obiad + wiersz.Podwieczorek + wiersz.Kolacja).ToString();

                
            }
        }

        private void ButWyczysc_Clicked(object sender, EventArgs e)
        {
            eLimitKal.Text = "";
            eSniadanie.Text = "";
            eIISniadanie.Text = "";
            eObiad.Text = "";
            ePodwieczorek.Text = "";
            eKolacja.Text = "";
        }

        private void DietaPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker modePicker = (Picker)sender;
             mode = modePicker.SelectedIndex;
            

            
            int Sniadanie = Convert.ToInt32(eSniadanie.Text);
            int IISniadanie = Convert.ToInt32(eIISniadanie.Text);
            int Obiad = Convert.ToInt32(eObiad.Text);
            int Podwieczorek = Convert.ToInt32(ePodwieczorek.Text);
            int Kolacja = Convert.ToInt32(eKolacja.Text);

            int suma = Sniadanie + IISniadanie + Obiad + Podwieczorek + Kolacja;
            float bialko, weglowodany, tluszcze;
            switch (mode)
            {
                case 0: //Normalna
                     dietaPicker.IsEnabled = true;
                     dietaPicker.IsVisible = true;
                     bialko =(float) 0.15 * suma;
                     weglowodany =(float) 0.55 * suma;
                     tluszcze = (float)0.30 * suma;

                    List<Entry> entries0 = new List<Entry>
                    {
                        new Entry(bialko)
                        {
                            Color = SKColor.Parse("#91288D"),
                            Label = "Białko",
                            ValueLabel = Convert.ToString(bialko)
                        },
                        new Entry(weglowodany)
                        {
                            Color = SKColor.Parse("#17AAC4"),
                            Label = "Węglowodany",
                            ValueLabel = Convert.ToString(weglowodany)
                        },
                        new Entry(tluszcze)
                        {
                            Color = SKColor.Parse("#C4AA17"),
                            Label = "Tłuszcze",
                            ValueLabel = Convert.ToString(tluszcze)
                        }

                    };
                    ChartCase0.Chart = new LineChart { Entries = entries0,  LabelTextSize = 25, LineSize = 3, LineAreaAlpha = 10, PointSize=18, PointMode = PointMode.Square, LineMode = LineMode.Straight  };
                    break;

                case 1: //Active
                    dietaPicker.IsEnabled = true;
                    dietaPicker.IsVisible = true;
                    bialko = (float)0.25 * suma;
                     weglowodany = (float)0.65 * suma;
                     tluszcze = (float)0.10 * suma;
                    List<Entry> entries1 = new List<Entry>
                    {
                        new Entry(bialko)
                        {
                            Color = SKColor.Parse("#91288D"),
                            Label = "Białko",
                            ValueLabel = Convert.ToString(bialko)
                        },
                        new Entry(weglowodany)
                        {
                            Color = SKColor.Parse("#17AAC4"),
                            Label = "Węglowodany",
                            ValueLabel = Convert.ToString(weglowodany)
                        },
                        new Entry(tluszcze)
                        {
                            Color = SKColor.Parse("#C4AA17"),
                            Label = "Tłuszcze",
                            ValueLabel = Convert.ToString(tluszcze)
                        }

                    };
                    ChartCase0.Chart = new LineChart { Entries = entries1, LabelTextSize = 25, LineSize = 3, LineAreaAlpha = 10, PointSize = 18, PointMode = PointMode.Square, LineMode = LineMode.Straight };
                    break;

                case 2: //Vege
                    dietaPicker.IsEnabled = true;
                    dietaPicker.IsVisible = true;
                    bialko = (float)0.20 * suma;
                    weglowodany = (float)0.57 * suma;
                    tluszcze = (float)0.23 * suma;
                    List<Entry> entries2 = new List<Entry>
                    {
                        new Entry(bialko)
                        {
                            Color = SKColor.Parse("#91288D"),
                            Label = "Białko",
                            ValueLabel = Convert.ToString(bialko)
                        },
                        new Entry(weglowodany)
                        {
                            Color = SKColor.Parse("#17AAC4"),
                            Label = "Węglowodany",
                            ValueLabel = Convert.ToString(weglowodany)
                        },
                        new Entry(tluszcze)
                        {
                            Color = SKColor.Parse("#C4AA17"),
                            Label = "Tłuszcze",
                            ValueLabel = Convert.ToString(tluszcze)
                        }

                    };
                    ChartCase0.Chart = new LineChart { Entries = entries2, LabelTextSize = 25, LineSize = 3, LineAreaAlpha = 10, PointSize = 18, PointMode = PointMode.Square, LineMode = LineMode.Straight };
                    break;


            }

          
            
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            Switch switchButton = sender as Switch;


            int sniadanie = Convert.ToInt32(eSniadanie.Text);
            int IIsniadanie = Convert.ToInt32(eIISniadanie.Text);
            int obiad = Convert.ToInt32(eObiad.Text);
            int podwieczorek = Convert.ToInt32(ePodwieczorek.Text);
            int kolacja = Convert.ToInt32(eKolacja.Text);

            int zapotrzebowanie = Convert.ToInt32(eLimitKal.Text);
            int suma = sniadanie + IIsniadanie + obiad + podwieczorek + kolacja;

            
            List<Entry> entries2 = new List<Entry>

            {

                new Entry((zapotrzebowanie-suma)/5)
                {

                    Color = SKColor.Parse("#FF1493"),
                    Label = "Sniadanie",
                    ValueLabel = Convert.ToString((zapotrzebowanie-suma)/5)
                },
                new Entry((zapotrzebowanie-suma)/5)
                {
                    Color = SKColor.Parse("#00BFFF"),
                    Label = "II Sniadanie",
                    ValueLabel = Convert.ToString((zapotrzebowanie-suma)/5)
                },
                new Entry((zapotrzebowanie-suma)/5)
                {
                    Color = SKColor.Parse("#00CED1"),
                    Label = "Obiad",
                    ValueLabel = Convert.ToString((zapotrzebowanie-suma)/5)
                },
                new Entry((zapotrzebowanie-suma)/5)
                {
                    Color = SKColor.Parse("#3C17C4"),
                    Label = "Podwieczorek",
                    ValueLabel = Convert.ToString((zapotrzebowanie-suma)/5)
                },
                new Entry((zapotrzebowanie-suma)/5)
                {
                    Color = SKColor.Parse("#6EFF14"),
                    Label = "Kolacja",
                    ValueLabel = Convert.ToString((zapotrzebowanie-suma)/5)
                }

            };

            if (zapotrzebowanie-suma>0)
            {
                if (switchButton.IsToggled)
                {
                    ChartToggled.Chart = new PointChart { Entries = entries2, LabelTextSize = 20};
                    ChartToggled.IsVisible = true;
                }
                else
                {
                    ChartToggled.IsVisible = false;
                }
            }
            else
            {
                DisplayAlert("Nadmiar kalorii", "Suma kalorii przekracza zakres limitu, nie można wygenerować wykresu uzupełniającego dietę", "Ok");
            }


        }
    }
}
