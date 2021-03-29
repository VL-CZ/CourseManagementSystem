import {Component, Input, OnInit} from '@angular/core';
import {PercentCalculator} from '../utils/percentCalculator';
import {GradeService} from '../grade.service';
import {GradeDetailsVM} from '../viewmodels/gradeDetailsVM';
import {RouterUtils} from '../utils/routerUtils';
import {ActivatedRoute, Router} from '@angular/router';
import {CourseMemberService} from '../course-member.service';

@Component({
  selector: 'app-student-grade-list',
  templateUrl: './student-grade-list.component.html',
  styleUrls: ['./student-grade-list.component.css']
})
export class StudentGradeListComponent implements OnInit {

  @Input()
  private userId: string;

  @Input()
  public isAdmin: boolean;

  public grades: GradeDetailsVM[] = [];
  private gradeService: GradeService;
  private courseMemberService: CourseMemberService;

  constructor(gradeService: GradeService, courseMemberService: CourseMemberService) {
    this.gradeService = gradeService;
    this.courseMemberService = courseMemberService;
  }

  ngOnInit() {
    this.courseMemberService.getGrades(this.userId).subscribe(grades => {
      this.grades = grades;
    });
  }

  public removeGrade(gradeId: number): void {
    this.gradeService.delete(gradeId).subscribe();
    this.grades = this.grades.filter(grade => grade.id !== gradeId);
  }

  public getPercentualScore(doubleValue: number) {
    return PercentCalculator.doubleToPercent(doubleValue, 2);
  }
}
