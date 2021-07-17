import {Component, Input, OnInit} from '@angular/core';
import {TestSubmissionInfoVM} from '../../viewmodels/testSubmissionVM';
import {CourseMemberService} from '../../services/course-member.service';
import {PercentStringFormatter} from '../../tools/percent-tools/percentStringFormatter';
import {DateTimeFormatter} from '../../tools/datetime/dateTimeFormatter';
import {PageNavigator} from '../../tools/pageNavigator';
import {Router} from '@angular/router';

/**
 * component representing student's test submissions
 */
@Component({
  selector: 'app-student-test-submissions',
  templateUrl: './student-test-submissions.component.html',
  styleUrls: ['./student-test-submissions.component.css']
})
export class StudentTestSubmissionsComponent implements OnInit {

  @Input()
  private courseMemberId: string;

  /**
   * list of student's test submissions
   */
  public testSubmissions: TestSubmissionInfoVM[] = [];

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

  private courseMemberService: CourseMemberService;

  constructor(courseMemberService: CourseMemberService, router: Router) {
    this.courseMemberService = courseMemberService;
    this.pageNavigator = new PageNavigator(router);
  }

  ngOnInit() {
    this.courseMemberService.getTestSubmissions(this.courseMemberId).subscribe(submissions => {
      this.testSubmissions = submissions;
    });
  }
}
