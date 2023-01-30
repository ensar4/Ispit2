using FIT_Api_Examples.Modul0_Autentifikacija.Models;
using FIT_Api_Examples.Modul2.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace FIT_Api_Examples.Modul3_MaticnaKnjiga.Models
{
    public class UpisAkademske
    {
        [Key]
        public int id { get; set; }
        public DateTime datumUpisa { get; set; }
        public DateTime? datumOvjere { get; set; }
        public int cijena { get; set; }
        public int godinaStudija { get; set; }
        public string napomena { get; set; }
        public bool? obnova { get; set; }

        [ForeignKey(nameof(student))]
        public int studentId { get; set; }
        public Student student { get; set; }

        [ForeignKey(nameof(korisnickiNalog))]
        public int korisnickiNalogId { get; set; }
        public KorisnickiNalog korisnickiNalog { get; set; }

        [ForeignKey(nameof(akademskaGodina))]
        public int akademskaGodinaId { get; set; }
        public AkademskaGodina akademskaGodina { get; set; }


    }
}
