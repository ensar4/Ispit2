import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {MojConfig} from "../moj-config";
import {HttpClient} from "@angular/common/http";

declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-student-maticnaknjiga',
  templateUrl: './student-maticnaknjiga.component.html',
  styleUrls: ['./student-maticnaknjiga.component.css']
})
export class StudentMaticnaknjigaComponent implements OnInit {
  studentId: any;
  noviSemestar:any;
  akademske:any;
  akademskeCMB: any;
  ovjeraNova: any;
  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute) {}

  ovjeriLjetni(s:any) {

  }

  upisLjetni(s:any) {

  }

  ovjeriZimski(s:any) {

  }
  getAkademske() :void
  {
    this.httpKlijent.get(MojConfig.adresa_servera+ "/UpisAkademske/getUpisAkademske?studentId=" + this.studentId, MojConfig.http_opcije()).subscribe(x=>{
      this.akademske = x;
    });
  }
  getAkademskeCMB() :void
  {
    this.httpKlijent.get(MojConfig.adresa_servera+ "/AkademskeGodine/GetAll_ForCmb", MojConfig.http_opcije()).subscribe(x=>{
      this.akademskeCMB = x;
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe((x:any)=>{
      this.studentId = +x["id"];
      this.getAkademske();
      this.getAkademskeCMB();
    })
  }
  post() {
    this.httpKlijent.post(MojConfig.adresa_servera + "/UpisAkademske/addAkademska", this.noviSemestar, MojConfig.http_opcije()).subscribe(x=>{this.getAkademske();this.noviSemestar=null});
  porukaSuccess("Uspjesno");
  }

  noviUpis() {
    this.noviSemestar={
      id: 0,
      datumUpisa: "2023-01-30T14:18:55.395Z",
      cijena: "",
      godinaStudija: "",
      obnova: true,
      studentId: this.studentId,
      akademskaGodinaId: "",

    }
  }
ovjera(x:any){
    this.ovjeraNova={
      datum: "2023-01-30T14:54:26.718Z",
      napomena: "ovjereno lagano",
      upisId:x.id
    }
}

  ovjeraPush() {
    this.httpKlijent.post(MojConfig.adresa_servera + "/UpisAkademske/Ovjeri?upisId="+ this.ovjeraNova.upisId, this.ovjeraNova ,MojConfig.http_opcije()).subscribe(x=>{this.ovjeraNova=null});
  porukaSuccess("uspjesno");
  }
}
