import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {CourseTestService} from '../course-test.service';
import {TestSubmitService} from '../test-submit.service';
import {TestSubmissionVM} from '../viewmodels/testSubmissionVM';

@Component({
  selector: 'app-test-submit',
  templateUrl: './test-submit.component.html',
  styleUrls: ['./test-submit.component.css']
})
export class TestSubmitComponent implements OnInit {

  private testSubmitService: TestSubmitService;
  public testSubmission: TestSubmissionVM;

  constructor(route: ActivatedRoute, courseTestService: CourseTestService, testSubmitService: TestSubmitService) {
    const testId = route.snapshot.paramMap.get('id');
    this.testSubmitService = testSubmitService;

    testSubmitService.getEmptySubmission(testId).subscribe(submission => {
      this.testSubmission = submission;
    });
  }

  ngOnInit() {
  }

  public submit() {
    this.testSubmitService.submit(this.testSubmission).subscribe(() => alert('Submitted'));
  }
}
