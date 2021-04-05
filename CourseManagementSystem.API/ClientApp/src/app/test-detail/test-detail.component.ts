import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {CourseTestDetailsVM} from '../viewmodels/courseTestVM';
import {CourseTestService} from '../course-test.service';
import {ActivatedRouteUtils} from '../utils/activatedRouteUtils';

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

  constructor(route: ActivatedRoute, courseTestService: CourseTestService) {
    this.testId = ActivatedRouteUtils.getIdParam(route);
    courseTestService.getById(this.testId).subscribe(result => {
      this.courseTest = result;
    });
  }

  ngOnInit() {
  }
}
