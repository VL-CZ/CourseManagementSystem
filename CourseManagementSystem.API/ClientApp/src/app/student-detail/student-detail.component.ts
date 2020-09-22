import {Component, Inject, OnInit} from '@angular/core';
import {Person, Student} from '../viewmodels/student';
import {HttpClient} from '@angular/common/http';
import {ActivatedRoute} from '@angular/router';
import {AddGradeVM} from '../viewmodels/addGradeVM';

@Component({
  selector: 'app-student-detail',
  templateUrl: './student-detail.component.html',
  styleUrls: ['./student-detail.component.css']
})
export class StudentDetailComponent implements OnInit {

  private student: Student;
  private http: HttpClient;
  private baseUrl: string;
  private newGrade: AddGradeVM;
  private userId: string;

  constructor(route: ActivatedRoute, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.userId = route.snapshot.paramMap.get('id');
    this.newGrade = new AddGradeVM(0, 'TEST XX', 'Comment XX');
    this.newGrade.topic = 'Topic';

    http.get<Student>(baseUrl + 'api/students/' + this.userId).subscribe(result => {
      this.student = result;
    }, error => console.error(error));
  }

  ngOnInit() {
  }

  public addGrade(): void {
    this.http.post<AddGradeVM>(this.baseUrl + 'api/students/' + this.student.id + '/assignGrade', this.newGrade).subscribe();

    this.http.get<Student>(this.baseUrl + 'api/students/' + this.userId).subscribe(result => {
      this.student = result;
    }, error => console.error(error));
  }
}
