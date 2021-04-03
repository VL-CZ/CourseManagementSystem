import {Component, Input, OnInit} from '@angular/core';
import {CourseTestService} from '../course-test.service';
import {TestSubmissionWithUserInfoVM} from '../viewmodels/testSubmissionWithUserInfoVM';
import {PercentCalculator} from '../utils/percentCalculator';

/**
 * component representing submitted solutions of the test
 */
@Component({
  selector: 'app-test-submission-list',
  templateUrl: './test-submission-list.component.html',
  styleUrls: ['./test-submission-list.component.css']
})
export class TestSubmissionListComponent implements OnInit {

  @Input()
  private testId: string;

  /**
   * list of test submissions with user name
   */
  public submissionsInfos: TestSubmissionWithUserInfoVM[] = [];

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
    this.courseTestService.getAllTestSubmissions(this.testId).subscribe(result => {
      this.submissionsInfos = result;
    });
  }

  /**
   * get percentual string from a double value
   * @param doubleValue percentual value in double format (0->0%, 1->100%)
   */
  public getPercentString(doubleValue: number): string {
    return PercentCalculator.doubleToPercent(doubleValue, 2).toString() + '%';
  }
}
