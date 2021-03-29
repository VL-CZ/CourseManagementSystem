import {Component, OnInit} from '@angular/core';
import {Student} from '../viewmodels/student';
import {ActivatedRoute} from '@angular/router';
import {CourseMemberService} from '../course-member.service';
import {GradeService} from '../grade.service';
import {RoleAuthService} from '../role-auth.service';
import {ActivatedRouteUtils} from '../utils/activatedRouteUtils';
import {PercentCalculator} from '../utils/percentCalculator';

@Component({
  selector: 'app-student-detail',
  templateUrl: './student-detail.component.html',
  styleUrls: ['./student-detail.component.css']
})
export class StudentDetailComponent implements OnInit {
  private gradeService: GradeService;

  public readonly userId: string;
  public isAdmin: boolean;
  public student: Student;

  constructor(route: ActivatedRoute, courseMemberService: CourseMemberService,
              gradeService: GradeService, roleAuthService: RoleAuthService) {
    this.gradeService = gradeService;

    this.userId = ActivatedRouteUtils.getIdParam(route);

    courseMemberService.getById(this.userId).subscribe(result => {
      this.student = result;
    });

    roleAuthService.isAdmin().subscribe(result => {
      this.isAdmin = result.isAdmin;
    });
  }

  ngOnInit() {
  }

  public removeGrade(gradeID: number): void {
    this.gradeService.delete(gradeID).subscribe();
    this.student.grades = this.student.grades.filter(g => g.id !== gradeID);
  }

  public getPercentualScore(doubleValue: number) {
    return PercentCalculator.doubleToPercent(doubleValue, 2);
  }
}
