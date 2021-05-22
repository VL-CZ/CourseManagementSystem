import {Component, Input, OnInit} from '@angular/core';
import {TestSubmissionInfoVM} from '../viewmodels/testSubmissionVM';
import {CourseMemberService} from '../course-member.service';
import {PercentStringFormatter} from '../utils/percentStringFormatter';
import {DateTimeFormatter} from '../utils/dateTimeFormatter';

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

  private courseMemberService: CourseMemberService;

  constructor(courseMemberService: CourseMemberService) {
    this.courseMemberService = courseMemberService;
  }

  ngOnInit() {
    this.courseMemberService.getTestSubmissions(this.courseMemberId).subscribe(submissions => {
      this.testSubmissions = submissions;
    });
  }
}
