import {Component, Input, OnInit} from '@angular/core';
import {CourseTestService} from '../../services/course-test.service';
import {TestSubmissionWithUserInfoVM} from '../../viewmodels/testSubmissionVM';
import {PercentStringFormatter} from '../../tools/percent-tools/percentStringFormatter';
import {DateTimeFormatter} from '../../tools/datetime/dateTimeFormatter';
import {PageNavigator} from '../../tools/pageNavigator';
import {Router} from '@angular/router';

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

  /**
   * class for navigating between the pages
   */
  public readonly pageNavigator: PageNavigator;

  private courseTestService: CourseTestService;

  constructor(courseTestService: CourseTestService, router: Router) {
    this.courseTestService = courseTestService;
    this.pageNavigator = new PageNavigator(router);
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
