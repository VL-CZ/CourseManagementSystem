import {Component, Input, OnInit} from '@angular/core';
import {AddGradeVM} from '../../viewmodels/gradeVM';
import {PercentCalculator} from '../../utils/percentCalculator';
import {CourseMemberService} from '../../services/course-member.service';
import {RouterUtils} from '../../utils/routerUtils';
import {ActivatedRoute, Router} from '@angular/router';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {ObservableWrapper} from '../../utils/observableWrapper';

/**
 * component representing form for adding a new grade
 */
@Component({
  selector: 'app-add-grade',
  templateUrl: './add-grade.component.html',
  styleUrls: ['./add-grade.component.css']
})
export class AddGradeComponent implements OnInit {

  /**
   * identifier of the course member
   */
  @Input()
  public courseMemberId: string;

  /**
   * grade that we will add to the student
   */
  public gradeToAdd: AddGradeVM;

  private readonly courseMemberService: CourseMemberService;
  private readonly activatedRoute: ActivatedRoute;
  private readonly router: Router;
  private bsModalRef: BsModalRef;
  private bsModalService: BsModalService;
  private observableWrapper: ObservableWrapper;

  constructor(courseMemberService: CourseMemberService, activatedRoute: ActivatedRoute, router: Router, bsModalService: BsModalService) {
    this.courseMemberService = courseMemberService;
    this.activatedRoute = activatedRoute;
    this.router = router;
    this.bsModalService = bsModalService;
    this.observableWrapper = new ObservableWrapper(this.bsModalRef, this.bsModalService);
    this.gradeToAdd = new AddGradeVM();
  }

  ngOnInit() {
  }

  /**
   * add the grade to the course member
   */
  public addGrade(): void {
    // divide percents by 100 -> get double
    this.gradeToAdd.percentualValue = PercentCalculator.percentToDouble(this.gradeToAdd.percentualValue);

    this.observableWrapper.subscribeOrShowError(
      this.courseMemberService.assignGrade(this.courseMemberId, this.gradeToAdd),
      () => {
        RouterUtils.reloadPage(this.router, this.activatedRoute);
      });
  }
}
