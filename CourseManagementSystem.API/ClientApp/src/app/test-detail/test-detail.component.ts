import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {CourseTestVM} from '../viewmodels/courseTestVM';
import {CourseTestService} from '../course-test.service';
import {ActivatedRouteUtils} from '../utils/activatedRouteUtils';

@Component({
  selector: 'app-test-detail',
  templateUrl: './test-detail.component.html',
  styleUrls: ['./test-detail.component.css']
})
export class TestDetailComponent implements OnInit {
  public courseTest: CourseTestVM;

  constructor(route: ActivatedRoute, courseTestService: CourseTestService) {
    const testId = ActivatedRouteUtils.getIdParam(route);
    courseTestService.getById(testId).subscribe(result => {
      this.courseTest = result;
    });
  }

  ngOnInit() {
  }
}
