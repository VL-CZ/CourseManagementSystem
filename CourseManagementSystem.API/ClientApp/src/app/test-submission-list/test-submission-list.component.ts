import {Component, Input, OnInit} from '@angular/core';
import {CourseTestService} from '../course-test.service';
import {TestSubmissionWithUserInfoVM} from '../viewmodels/testSubmissionVM';
import {PercentStringFormatter} from '../utils/percentStringFormatter';
import {DateTimeFormatter} from '../utils/dateTimeFormatter';

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

  /**
   * formatter of percent strings
   */
  public percentStringFormatter: PercentStringFormatter = new PercentStringFormatter();

  /**
   * formatter of date-time
   */
  public dateTimeFormatter: DateTimeFormatter = new DateTimeFormatter();

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
}
