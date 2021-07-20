import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {CourseTestService} from '../../services/course-test.service';
import {AddCourseTestVM, CourseTestDetailsVM} from '../../viewmodels/courseTestVM';
import {TestQuestionVM} from '../../viewmodels/testQuestionVM';
import {ArrayTools} from '../../tools/arrayTools';
import {ActivatedRouteTools} from '../../tools/activatedRouteTools';
import {TestQuestionNumberSetter} from '../../tools/testQuestionNumberSetter';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {DateTimeBinder} from '../../tools/datetime/dateTimeBinder';
import {ObservableWrapper} from '../../tools/observableWrapper';
import {PageNavigator} from '../../tools/pageNavigator';

/**
 * component for creating a test
 */
@Component({
  selector: 'app-test-create',
  templateUrl: './test-create.component.html',
  styleUrls: ['./test-create.component.css']
})
export class TestCreateComponent implements OnInit {

  /**
   * test that will be created
   */
  public testToCreate: AddCourseTestVM;

  /**
   * number of questions in the test
   */
  public questionCount = 0;

  /**
   * class for binding date-time values
   */
  public dateTimeBinder = new DateTimeBinder();

  public readonly courseId: string;
  private courseTestService: CourseTestService;
  private readonly pageNavigator: PageNavigator;
  private bsModalRef: BsModalRef;
  private modalService: BsModalService;
  private observableWrapper: ObservableWrapper;

  constructor(route: ActivatedRoute, courseTestService: CourseTestService, router: Router, bsModalService: BsModalService) {
    this.courseId = ActivatedRouteTools.getIdParam(route);
    this.courseTestService = courseTestService;
    this.pageNavigator = new PageNavigator(router);
    this.modalService = bsModalService;

    this.testToCreate = new AddCourseTestVM();
    this.observableWrapper = new ObservableWrapper(this.bsModalRef, this.modalService);
  }

  ngOnInit() {
  }

  /**
   * create the test
   */
  public createTest(): void {
    this.testToCreate.deadline = this.dateTimeBinder.toString();
    this.observableWrapper.subscribeOrShowError(
      this.courseTestService.addToCourse(this.testToCreate, this.courseId),
      () => {
        this.pageNavigator.navigateToCourseDetail(this.courseId);
      });
  }

  /**
   * update number of questions in the test
   * @see questionCount
   */
  public updateQuestionCount(): void {
    const questions = this.testToCreate.questions;
    const instance = new TestQuestionVM();
    ArrayTools.resize<TestQuestionVM>(questions, this.questionCount, instance);

    TestQuestionNumberSetter.setQuestionNumbers(questions);
  }
}
