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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FIT_Api_Examples.Modul2.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult addStudent([FromBody] StudentAddVM x)
        {
            Student student;
            if (x.id == 0)
            {
                student = new Student()
                {
                    slika_korisnika = Config.SlikeURL + "empty.png",
                    created_time = DateTime.Now
                };
                _dbContext.Add(student);
            }
            else
                student = _dbContext.Student.Find(x.id);

            student.ime = x.ime;
            student.prezime = x.prezime;
            student.broj_indeksa = "IB20000" + x.opstina_rodjenja_id;
           
            student.opstina_rodjenja_id = x.opstina_rodjenja_id;


            _dbContext.SaveChanges();
            return Ok();
        }
      

        [HttpGet]
        public ActionResult<List<Student>> GetAll(string ime_prezime)
        {
            var data = _dbContext.Student
                     .Include(s => s.opstina_rodjenja.drzava)
                     .Where(x => ime_prezime == null || (x.ime + " " + x.prezime)
                     .StartsWith(ime_prezime) || (x.prezime + " " + x.ime)
                     .StartsWith(ime_prezime))
                     .OrderByDescending(s => s.id)
                     .AsQueryable();


            return data.Take(100).ToList();
        }

        [HttpPost]
        public ActionResult deleteStudent(int studentId)
        {
            if (studentId == 0)
            {
                return BadRequest("bad req");
            }
            else
            {
                var student = _dbContext.Student.Find(studentId);
                _dbContext.Remove(student);
                _dbContext.SaveChanges();
            }
            return Ok();
        }

    }
}
