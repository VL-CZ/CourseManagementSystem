import {Component, Inject, OnInit, Output} from '@angular/core';
import {Student} from '../viewmodels/student';
import {HttpClient} from '@angular/common/http';
import {ActivatedRoute} from '@angular/router';
import {AddGradeVM} from '../viewmodels/addGradeVM';
import {IsAdminVM} from '../viewmodels/isAdminVM';
import {PersonService} from '../person.service';
import {GradeService} from '../grade.service';

@Component({
  selector: 'app-student-detail',
  templateUrl: './student-detail.component.html',
  styleUrls: ['./student-detail.component.css']
})
export class StudentDetailComponent implements OnInit {

  private http: HttpClient;
  private baseUrl: string;
  private userId: string;
  private personService: PersonService;
  private gradeService: GradeService;

  public isAdmin: boolean;
  public student: Student;
  public newGrade: AddGradeVM;

  constructor(route: ActivatedRoute, http: HttpClient, @Inject('BASE_URL') baseUrl: string, personService: PersonService, gradeService: GradeService) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.personService = personService;
    this.gradeService = gradeService;

    this.userId = route.snapshot.paramMap.get('id');
    this.newGrade = new AddGradeVM();

    personService.getById(this.userId).subscribe(result => {
      this.student = result;
    });

    http.get<IsAdminVM>(baseUrl + 'api/students/isAdmin').subscribe(result => {
      this.isAdmin = result.isAdmin;
    }, error => console.error(error));
  }

  ngOnInit() {
  }

  public addGrade(): void {
    this.personService.assignGrade(this.student.id, this.newGrade).subscribe(
      result => {
        this.student.grades.push(result);
      });

    this.newGrade = new AddGradeVM();
  }

  public removeGrade(gradeID: number): void {
    this.gradeService.delete(gradeID).subscribe();
    this.student.grades = this.student.grades.filter(g => g.id !== gradeID);
  }
}
