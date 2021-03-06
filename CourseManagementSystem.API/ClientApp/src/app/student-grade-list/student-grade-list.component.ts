import {Component, Input, OnInit} from '@angular/core';
import {PercentCalculator} from '../utils/percentCalculator';
import {GradeService} from '../grade.service';
import {GradeDetailsVM} from '../viewmodels/gradeVM';
import {CourseMemberService} from '../course-member.service';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {ConfirmDialogComponent} from '../confirm-dialog/confirm-dialog.component';

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
  private bsModalRef: BsModalRef;
  private modalService: BsModalService;

  constructor(gradeService: GradeService, courseMemberService: CourseMemberService, modalService: BsModalService) {
    this.gradeService = gradeService;
    this.courseMemberService = courseMemberService;
    this.modalService = modalService;
  }

  ngOnInit() {
    this.reloadGrades();
  }

  /**
   * remove a grade with given id
   * @param gradeId identifier of the grade
   */
  public removeGrade(gradeId: string): void {
    // this.gradeService.delete(gradeId).subscribe();
    // this.grades = this.grades.filter(grade => grade.id !== gradeId);

    const initialState = {
      title: 'Delete a grade',
      text: 'Are you sure you want to delete this grade?',
      onConfirm: () => {
        this.gradeService.delete(gradeId).subscribe(() => {
          this.reloadGrades();
        });
      }
    };
    this.bsModalRef = this.modalService.show(ConfirmDialogComponent, {initialState});
  }

  /**
   * get percentual string from a double value
   * @param doubleValue percentual value in double format (0->0%, 1->100%)
   */
  public getPercentString(doubleValue: number): string {
    return PercentCalculator.doubleToPercent(doubleValue, 2).toString() + '%';
  }

  /**
   * reload data about grades
   * @private
   */
  private reloadGrades() {
    this.courseMemberService.getGrades(this.courseMemberId).subscribe(grades => {
      this.grades = grades;
    });
  }
}
