import {Component, Inject, OnInit, Output} from '@angular/core';
import {Person, Student} from '../viewmodels/student';
import {HttpClient} from '@angular/common/http';
import {ActivatedRoute} from '@angular/router';
import {AddGradeVM} from '../viewmodels/addGradeVM';
import {Grade} from '../viewmodels/grade';

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
    this.newGrade = new AddGradeVM();

    http.get<Student>(baseUrl + 'api/students/' + this.userId).subscribe(result => {
      this.student = result;
    }, error => console.error(error));
  }

  ngOnInit() {
  }

  public addGrade(): void {
    let g;
    this.http.post(this.baseUrl + 'api/students/' + this.student.id + '/assignGrade', this.newGrade).subscribe(
      result => {
          g = result;
          this.student.grades.push(g); }
      , error => console.error(error)
    );

    this.newGrade = new AddGradeVM();
  }

  public removeGrade(gradeID: number): void {
    this.http.delete(this.baseUrl + 'api/grades/delete/' + gradeID).subscribe();
    this.student.grades = this.student.grades.filter(g => g.id !== gradeID);
  }
}
