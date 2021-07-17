import {Component, Input, OnInit} from '@angular/core';
import {GradeService} from '../../services/grade.service';
import {GradeDetailsVM} from '../../viewmodels/gradeVM';
import {CourseMemberService} from '../../services/course-member.service';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {ConfirmDialogComponent} from '../confirm-dialog/confirm-dialog.component';
import {PercentStringFormatter} from '../../utils/percentStringFormatter';
import {RouterUtils} from '../../utils/routerUtils';
import {ActivatedRoute, Router} from '@angular/router';
import {ConfirmDialogManager} from '../../utils/confirmDialogManager';

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
  private router: Router;
  private activatedRoute: ActivatedRoute;

  constructor(gradeService: GradeService, courseMemberService: CourseMemberService, modalService: BsModalService,
              router: Router, activatedRoute: ActivatedRoute) {
    this.gradeService = gradeService;
    this.courseMemberService = courseMemberService;
    this.modalService = modalService;
    this.activatedRoute = activatedRoute;
    this.router = router;
  }

  ngOnInit() {
    this.reloadGrades();
  }

  /**
   * remove a grade with given id
   * @param gradeId identifier of the grade
   */
  public removeGrade(gradeId: string): void {

    const confirmDialogManager = new ConfirmDialogManager(this.bsModalRef, this.modalService);

    confirmDialogManager.displayDialog(
      'Delete a grade',
      'Are you sure you want to delete this grade?',
      () => {
        this.gradeService.delete(gradeId).subscribe(() => {
          RouterUtils.reloadPage(this.router, this.activatedRoute);
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
