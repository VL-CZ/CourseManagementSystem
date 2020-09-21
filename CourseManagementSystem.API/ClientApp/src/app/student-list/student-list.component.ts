import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import {Person} from '../viewmodels/student';

@Component({
  templateUrl: './student-list.component.html',
  styleUrls: ['./student-list.component.css']
})
export class StudentListComponent implements OnInit {

  public people: Person[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Person[]>(baseUrl + 'api/students').subscribe(result => {
      this.people = result;
    }, error => console.error(error));
  }

  ngOnInit() {
  }

}
