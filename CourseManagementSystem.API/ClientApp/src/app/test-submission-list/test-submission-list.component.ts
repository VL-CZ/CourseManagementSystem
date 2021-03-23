import {Component, Input, OnInit} from '@angular/core';
import {CourseTestService} from '../course-test.service';
import {TestSubmissionWithUserInfoVM} from '../viewmodels/testSubmissionWithUserInfoVM';
import {PercentCalculator} from '../utils/percentCalculator';

@Component({
  selector: 'app-test-submission-list',
  templateUrl: './test-submission-list.component.html',
  styleUrls: ['./test-submission-list.component.css']
})
export class TestSubmissionListComponent implements OnInit {

  @Input()
  private testId: number;

  public submissionsInfo: TestSubmissionWithUserInfoVM[] = [];
  private courseTestService: CourseTestService;

  constructor(courseTestService: CourseTestService) {
    this.courseTestService = courseTestService;
  }

  ngOnInit() {
    this.reloadData();
  }

  /**
   * reload data about test submissions
   */
  public reloadData(): void {
    this.courseTestService.getAllTestSubmissions(this.testId.toString()).subscribe(result => {
      this.submissionsInfo = result;
    });
  }

  public getPercentualScore(doubleValue: number) {
    return PercentCalculator.doubleToPercent(doubleValue, 2);
  }

}
