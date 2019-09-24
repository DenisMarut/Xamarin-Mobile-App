using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace LicznikKalorii
{
    class TabKcal
    {
        [PrimaryKey, AutoIncrement]
        public int Id_pom { get; set; }
        public int Zapotrzebowanie { get; set; }
        public int Sniadanie { get; set; }
        public int IISniadanie {get; set; }
        public int Obiad { get; set; }
        public int Podwieczorek { get; set; }
        public int Kolacja { get; set; }
    }
}
