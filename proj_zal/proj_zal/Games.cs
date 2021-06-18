using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace proj_zal
{
    [Table("tSteamChart")]
    public class Games
    {
        [Ignore]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Name("<gamename>")]
        public string gamename { get; set; }

        [Name("<year>")]
        public int year { get; set; }

        [Name("<month>")]
        public string month { get; set; }

        [Name("<avg>")]
        public decimal avg { get; set; }

        [Name("<gain>")]
        public string gain { get; set; }

        [Name("<peak>")]
        public int peak { get; set; }

        [Name("<avg_peak_perc>")]
        public string avg_peak_perc { get; set; }
    }
}