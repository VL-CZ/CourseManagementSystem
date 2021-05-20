import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {CourseTestService} from '../course-test.service';
import {AddCourseTestVM, CourseTestDetailsVM} from '../viewmodels/courseTestVM';
import {TestQuestionVM} from '../viewmodels/testQuestionVM';
import {ArrayUtils} from '../utils/arrayUtils';
import {ActivatedRouteUtils} from '../utils/activatedRouteUtils';
import {TestQuestionNumberSetter} from '../utils/testQuestionNumberSetter';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {ErrorModalManager} from '../utils/errorModalManager';

/**
 * component for creating a test
 */
@Component({
  selector: 'app-test-create',
  templateUrl: './test-create.component.html',
  styleUrls: ['./test-create.component.css']
})
export class TestCreateComponent implements OnInit {
  private readonly courseId: string;
  private courseTestService: CourseTestService;
  private router: Router;
  private bsModalRef: BsModalRef;
  private modalService: BsModalService;
  private errorModalManager: ErrorModalManager;

  /**
   * test that will be created
   */
  public testToCreate: AddCourseTestVM;

  /**
   * number of questions in the test
   */
  public questionCount = 0;

  constructor(route: ActivatedRoute, courseTestService: CourseTestService, router: Router, bsModalService: BsModalService) {
    this.courseId = ActivatedRouteUtils.getIdParam(route);
    this.courseTestService = courseTestService;
    this.router = router;
    this.modalService = bsModalService;

    this.testToCreate = new AddCourseTestVM();
    this.errorModalManager = new ErrorModalManager(this.bsModalRef, this.modalService);
  }

  ngOnInit() {
  }

  /**
   * create the test
   */
  public createTest(): void {
    this.courseTestService.addToCourse(this.testToCreate, this.courseId).subscribe(() => {
        this.router.navigate(['/courses', this.courseId]);
      },
      err => this.errorModalManager.displayError(err)
    );
  }

  /**
   * update number of questions in the test
   * @see questionCount
   */
  public updateQuestionCount(): void {
    const questions = this.testToCreate.questions;
    const instance = new TestQuestionVM();
    ArrayUtils.resize<TestQuestionVM>(questions, this.questionCount, instance);

    TestQuestionNumberSetter.setQuestionNumbers(questions);
  }
}
