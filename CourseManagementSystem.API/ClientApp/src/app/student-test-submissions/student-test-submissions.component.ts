import {Component, Input, OnInit} from '@angular/core';
import {TestSubmissionInfoVM} from '../viewmodels/testSubmisionInfoVM';
import {CourseMemberService} from '../course-member.service';
import {PercentCalculator} from '../utils/percentCalculator';

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

  private courseMemberService: CourseMemberService;

  constructor(courseMemberService: CourseMemberService) {
    this.courseMemberService = courseMemberService;
  }

  ngOnInit() {
    this.courseMemberService.getTestSubmissions(this.courseMemberId).subscribe(submissions => {
      this.testSubmissions = submissions;
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
