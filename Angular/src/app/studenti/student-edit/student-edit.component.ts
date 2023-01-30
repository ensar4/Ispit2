import {Component, Input, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../../moj-config";

declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;
@Component({
  selector: 'app-student-edit',
  templateUrl: './student-edit.component.html',
  styleUrls: ['./student-edit.component.css']
})
export class StudentEditComponent implements OnInit {
   opstineCmb: any;


  constructor(private httpKlijent: HttpClient) { }
  @Input()
  odabraniStudent:any;

  ngOnInit(): void {
    this.getOpstine();
  }

  getOpstine() :void
  {
    this.httpKlijent.get(MojConfig.adresa_servera+ "/Opstina/GetByAll", MojConfig.http_opcije()).subscribe(x=>{
      this.opstineCmb = x;
    });
  }

  spasiStudenta() {
    this.httpKlijent.post(MojConfig.adresa_servera + "/Student/addStudent", this.odabraniStudent, MojConfig.http_opcije()).subscribe(x=>{
      this.odabraniStudent=null;

      porukaSuccess("dodan - need refresh");

    });
  }
}
