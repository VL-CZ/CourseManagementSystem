import {Component, Input, OnInit} from '@angular/core';
import {PercentCalculator} from '../utils/percentCalculator';
import {GradeService} from '../grade.service';
import {GradeDetailsVM} from '../viewmodels/gradeDetailsVM';
import {CourseMemberService} from '../course-member.service';

/**
 * component with list of student's grades
 */
@Component({
  selector: 'app-student-grade-list',
  templateUrl: './student-grade-list.component.html',
  styleUrls: ['./student-grade-list.component.css']
})
export class StudentGradeListComponent implements OnInit {

  /**
   * is the current course member admin?
   */
  @Input()
  public isAdmin: boolean;

  /**
   * identifier of the current course member
   * @private
   */
  @Input()
  private courseMemberId: string;

  /**
   * list of student's grades
   */
  public grades: GradeDetailsVM[] = [];

  private gradeService: GradeService;
  private courseMemberService: CourseMemberService;

  constructor(gradeService: GradeService, courseMemberService: CourseMemberService) {
    this.gradeService = gradeService;
    this.courseMemberService = courseMemberService;
  }

  ngOnInit() {
    this.courseMemberService.getGrades(this.courseMemberId).subscribe(grades => {
      this.grades = grades;
    });
  }

  /**
   * remove a grade with given id
   * @param gradeId identifier of the grade
   */
  public removeGrade(gradeId: number): void {
    this.gradeService.delete(gradeId).subscribe();
    this.grades = this.grades.filter(grade => grade.id !== gradeId);
  }

  /**
   * get percentual string from a double value
   * @param doubleValue percentual value in double format (0->0%, 1->100%)
   */
  public getPercentString(doubleValue: number): string {
    return PercentCalculator.doubleToPercent(doubleValue, 2).toString() + '%';
  }
}
