import {Component, Input, OnInit} from '@angular/core';
import {GradeService} from '../../services/grade.service';
import {GradeDetailsVM} from '../../viewmodels/gradeVM';
import {CourseMemberService} from '../../services/course-member.service';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {ConfirmDialogComponent} from '../confirm-dialog/confirm-dialog.component';
import {PercentStringFormatter} from '../../tools/percent-tools/percentStringFormatter';
import {PageNavigator} from '../../tools/pageNavigator';
import {ActivatedRoute, Router} from '@angular/router';
import {ConfirmDialogManager} from '../../tools/dialog-managers/confirmDialogManager';

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

  /**
   * formatter of percent strings
   */
  public percentStringFormatter: PercentStringFormatter = new PercentStringFormatter();

  private gradeService: GradeService;
  private courseMemberService: CourseMemberService;
  private bsModalRef: BsModalRef;
  private modalService: BsModalService;
  private pageNavigator: PageNavigator;
  private activatedRoute: ActivatedRoute;
  private confirmDialogManager: ConfirmDialogManager;

  constructor(gradeService: GradeService, courseMemberService: CourseMemberService, modalService: BsModalService,
              router: Router, activatedRoute: ActivatedRoute) {
    this.gradeService = gradeService;
    this.courseMemberService = courseMemberService;
    this.modalService = modalService;
    this.activatedRoute = activatedRoute;
    this.pageNavigator = new PageNavigator(router);
    this.confirmDialogManager = new ConfirmDialogManager(this.bsModalRef, this.modalService);
  }

  ngOnInit() {
    this.reloadGrades();
  }

  /**
   * remove a grade with given id
   * @param gradeId identifier of the grade
   */
  public removeGrade(gradeId: string): void {
    this.confirmDialogManager.displayDialog(
      'Delete a grade',
      'Are you sure you want to delete this grade?',
      () => {
        this.gradeService.delete(gradeId).subscribe(() => {
          this.pageNavigator.reloadCurrentPage(this.activatedRoute);
        });
      });
  }

  /**
   * reload data about grades
   * @private
   */
  private reloadGrades(): void {
    this.courseMemberService.getGrades(this.courseMemberId).subscribe(grades => {
      this.grades = grades;
    });
  }
}
