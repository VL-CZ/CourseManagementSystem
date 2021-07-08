import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {CourseTestDetailsVM} from '../../viewmodels/courseTestVM';
import {CourseTestService} from '../../services/course-test.service';
import {ActivatedRouteUtils} from '../../utils/activatedRouteUtils';
import {DateTimeFormatter} from '../../utils/dateTimeFormatter';
import {CourseTestUtils} from '../../utils/courseTestUtils';

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

  public courseTestUtils: CourseTestUtils = new CourseTestUtils();

  constructor(route: ActivatedRoute, courseTestService: CourseTestService) {
    this.testId = ActivatedRouteUtils.getIdParam(route);
    courseTestService.getById(this.testId).subscribe(result => {
      this.courseTest = result;
    });
  }

  ngOnInit() {
  }
}
