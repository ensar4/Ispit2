using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.Modul0_Autentifikacija.Models;
using FIT_Api_Examples.Modul2.Models;
using FIT_Api_Examples.Modul2.ViewModels;
using FIT_Api_Examples.Modul3_MaticnaKnjiga.Models;
using FIT_Api_Examples.Modul3_MaticnaKnjiga.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FIT_Api_Examples.Modul2.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class UpisAkademskeController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public UpisAkademskeController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult addAkademska([FromBody] UpisAkademskeVM x)
        {
            var lista = _dbContext.UpisAkademske.Where(s=>s.studentId==x.studentId).ToList();

            foreach (var item in lista)
            {
                if (item.godinaStudija==x.godinaStudija&&x.obnova==false)
                {
                    return BadRequest("ne radi");
                }
            }

            UpisAkademske akGodina;

            akGodina=new UpisAkademske() {
                id = x.id,
                studentId =x.studentId,
                godinaStudija=x.godinaStudija,
                akademskaGodinaId=x.akademskaGodinaId,
                cijena=x.cijena,
                korisnickiNalog=HttpContext.GetLoginInfo().korisnickiNalog,
                datumUpisa=x.datumUpisa,
                obnova=x.obnova
            };

            _dbContext.Add(akGodina);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public ActionResult getUpisAkademske(int studentId)
        {
            var s = _dbContext.Student.Find(studentId);

            List<UpisAkademske> upis = _dbContext.UpisAkademske.Include(s => s.korisnickiNalog).Include(s => s.akademskaGodina)
                .Where(s => s.studentId == studentId).ToList();

            return Ok(
                new
                {
                    s,upis
                }
                );
        }

        public class OvjeriVM
        {
            public DateTime datum { get; set; }
            public string napomena { get; set; }
        }

        [HttpPost]
        public ActionResult Ovjeri(int upisId, OvjeriVM o)
        {
            var upis = _dbContext.UpisAkademske.SingleOrDefault(s => s.id == upisId);

            upis.id = upisId;
            upis.datumOvjere = o.datum;
            upis.napomena = o.napomena;

            _dbContext.SaveChanges();
            return Ok();

        }

    }
}
