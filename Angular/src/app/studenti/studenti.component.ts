import { Component, OnInit } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {Router} from "@angular/router";
declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-studenti',
  templateUrl: './studenti.component.html',
  styleUrls: ['./studenti.component.css']
})
export class StudentiComponent implements OnInit {

  title:string = 'angularFIT2';
  ime_prezime:string = '';
  opstina: string = '';
  studentPodaci: any;
  filter_ime_prezime: boolean;
  filter_opstina: boolean;
  odabraniStudent:any;

  constructor(private httpKlijent: HttpClient, private router: Router) {
  }

  testirajWebApi() :void
  {
    this.httpKlijent.get(MojConfig.adresa_servera+ "/Student/GetAll", MojConfig.http_opcije()).subscribe(x=>{
      this.studentPodaci = x;
    });
  }

  ngOnInit(): void {
    this.testirajWebApi();
  }



  getPodaci() {
    return this.studentPodaci.filter((x:any)=>(!this.filter_ime_prezime
      ||
        (x.ime + " " + x.prezime).toLowerCase().startsWith(this.ime_prezime.toLowerCase())
      ||
        (x.ime + " " + x.prezime).toLowerCase().startsWith(this.ime_prezime.toLowerCase())
      )
      &&
      (!this.filter_opstina
      ||
      (x.opstina_rodjenja.description).toLowerCase().startsWith(this.opstina.toLowerCase()))
    )
  }

  obrisi(s: any) {
    this.httpKlijent.post(MojConfig.adresa_servera + "/Student/deleteStudent?studentId=" + s.id, MojConfig.http_opcije()).subscribe(x=>{this.testirajWebApi()});
  }

  uredi(s: any) {
    this.odabraniStudent=s;
  }

  dodaj() {
    this.odabraniStudent={
      id: 0,
      ime: this.ime_prezime,
      prezime: "",
      opstina_rodjenja_id: 1,
    }
  }

  otvoriMaticnu(s: any) {
    this.router.navigate(['student-maticnaknjiga', s.id])
  }
}
