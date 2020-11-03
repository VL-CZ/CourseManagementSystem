import {Component, OnInit} from '@angular/core';
import {Student} from '../viewmodels/student';
import {ActivatedRoute} from '@angular/router';
import {AddGradeVM} from '../viewmodels/addGradeVM';
import {CourseMemberService} from '../course-member.service';
import {GradeService} from '../grade.service';
import {RoleAuthService} from '../role-auth.service';

@Component({
  selector: 'app-student-detail',
  templateUrl: './student-detail.component.html',
  styleUrls: ['./student-detail.component.css']
})
export class StudentDetailComponent implements OnInit {

  private userId: string;
  private personService: CourseMemberService;
  private gradeService: GradeService;

  public isAdmin: boolean;
  public student: Student;
  public newGrade: AddGradeVM;

  constructor(route: ActivatedRoute, personService: CourseMemberService, gradeService: GradeService, roleAuthService: RoleAuthService) {
    this.personService = personService;
    this.gradeService = gradeService;

    this.userId = route.snapshot.paramMap.get('id');
    this.newGrade = new AddGradeVM();

    this.personService.getById(this.userId).subscribe(result => {
      this.student = result;
    });

    roleAuthService.isAdmin().subscribe(result => {
      this.isAdmin = result.isAdmin;
    });
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
