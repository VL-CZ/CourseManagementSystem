import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import {Grade} from '../viewmodels/grade';

@Component({
  selector: 'app-grade-list',
  templateUrl: './grade-list.component.html',
  styleUrls: ['./grade-list.component.css']
})
export class GradeListComponent implements OnInit {

  public grades: Grade[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Grade[]>(baseUrl + 'api/grades').subscribe(result => {
      this.grades = result;
    }, error => console.error(error));
  }

  ngOnInit() {
  }

}
