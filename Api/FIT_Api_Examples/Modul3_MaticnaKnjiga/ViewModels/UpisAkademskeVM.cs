using FIT_Api_Examples.Modul0_Autentifikacija.Models;
using FIT_Api_Examples.Modul2.Models;
using FIT_Api_Examples.Modul3_MaticnaKnjiga.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace FIT_Api_Examples.Modul3_MaticnaKnjiga.ViewModels
{
    public class UpisAkademskeVM
    {
        public int id { get; set; }
        public DateTime datumUpisa { get; set; }
        public int cijena { get; set; }
        public int godinaStudija { get; set; }
        public bool? obnova { get; set; }
        public int studentId { get; set; }
        public int korisnickiNalogId { get; set; }
        public int akademskaGodinaId { get; set; }
    }
}
