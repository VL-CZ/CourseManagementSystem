import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {CourseTest} from '../viewmodels/courseTest';
import {CourseTestService} from '../course-test.service';

@Component({
  selector: 'app-test-detail',
  templateUrl: './test-detail.component.html',
  styleUrls: ['./test-detail.component.css']
})
export class TestDetailComponent implements OnInit {
  public courseTest: CourseTest;

  constructor(route: ActivatedRoute, courseTestService: CourseTestService) {
    const testId = route.snapshot.paramMap.get('id');
    courseTestService.getById(testId).subscribe(result => {
      this.courseTest = result;
    });
  }

  ngOnInit() {
  }
}
