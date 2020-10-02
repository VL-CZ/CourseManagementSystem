import {HttpClient} from '@angular/common/http';
import {Component, Inject, OnInit} from '@angular/core';
import {Person} from '../viewmodels/student';
import {IsAdminVM} from '../viewmodels/isAdminVM';

@Component({
  templateUrl: './student-list.component.html',
  styleUrls: ['./student-list.component.css']
})
export class StudentListComponent implements OnInit {

  public people: Person[];
  private isAdmin: boolean;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Person[]>(baseUrl + 'api/students').subscribe(result => {
      this.people = result;
    }, error => console.error(error));

    http.get<IsAdminVM>(baseUrl + 'api/students/isAdmin').subscribe(result => {
      this.isAdmin = result.isAdmin;
    }, error => console.error(error));
  }

  ngOnInit() {
  }

}
