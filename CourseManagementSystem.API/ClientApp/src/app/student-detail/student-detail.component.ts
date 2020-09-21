import {Component, Inject, OnInit} from '@angular/core';
import {Person, Student} from '../viewmodels/student';
import {HttpClient} from '@angular/common/http';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-student-detail',
  templateUrl: './student-detail.component.html',
  styleUrls: ['./student-detail.component.css']
})
export class StudentDetailComponent implements OnInit {

  private student: Student;

  constructor(route: ActivatedRoute, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    const id = route.snapshot.paramMap.get('id');

    http.get<Student>(baseUrl + 'api/students/' + id).subscribe(result => {
      this.student = result;
    }, error => console.error(error));
  }

  ngOnInit() {
  }

}
