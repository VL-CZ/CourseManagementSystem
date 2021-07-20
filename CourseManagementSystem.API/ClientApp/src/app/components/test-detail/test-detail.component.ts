import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {CourseTestDetailsVM, TestStatus} from '../../viewmodels/courseTestVM';
import {CourseTestService} from '../../services/course-test.service';
import {ActivatedRouteTools} from '../../tools/activatedRouteTools';
import {DateTimeFormatter} from '../../tools/datetime/dateTimeFormatter';
import {CourseTestTools} from '../../tools/courseTestTools';
import {PageNavigator} from '../../tools/pageNavigator';

/**
 * component representing detail of the test
 */
@Component({
  selector: 'app-test-detail',
  templateUrl: './test-detail.component.html',
  styleUrls: ['./test-detail.component.css']
})
export class TestDetailComponent implements OnInit {
  /**
   * current course test
   */
  public courseTest: CourseTestDetailsVM = new CourseTestDetailsVM();

  /**
   * id of the test
   */
  public testId: string;

  /**
   * formatter of date-time
   */
  public dateTimeFormatter: DateTimeFormatter = new DateTimeFormatter();

  /**
   * id of the course that this test belongs to
   */
  public courseId: string;

  /**
   * class for navigating between the pages
   */
  public readonly pageNavigator: PageNavigator;

  public courseTestTools: CourseTestTools = new CourseTestTools();

  constructor(route: ActivatedRoute, router: Router, courseTestService: CourseTestService) {
    this.testId = ActivatedRouteTools.getIdParam(route);
    this.pageNavigator = new PageNavigator(router);

    courseTestService.getCourseId(this.testId).subscribe(result => {
      this.courseId = result.value;
    });

    courseTestService.getById(this.testId).subscribe(result => {
      this.courseTest = result;
    });
  }

  ngOnInit() {
  }

  /**
   * check if {@link courseTest} can be edited
   */
  public canBeEdited(): boolean {
    return this.courseTest.status === TestStatus.New;
  }
}
