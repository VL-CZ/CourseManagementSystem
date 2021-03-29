import {Component, Input, OnInit} from '@angular/core';
import {TestSubmissionInfoVM} from '../viewmodels/testSubmisionInfoVM';
import {CourseMemberService} from '../course-member.service';
import {PercentCalculator} from '../utils/percentCalculator';

@Component({
  selector: 'app-student-test-submissions',
  templateUrl: './student-test-submissions.component.html',
  styleUrls: ['./student-test-submissions.component.css']
})
export class StudentTestSubmissionsComponent implements OnInit {

  @Input()
  private userId: string;
  public testSubmissions: TestSubmissionInfoVM[] = [];

  constructor(courseMemberService: CourseMemberService) {
    courseMemberService.getTestSubmissions(this.userId).subscribe(submissions => {
      this.testSubmissions = submissions;
    });
  }

  ngOnInit() {
  }

  public getPercentualScore(doubleValue: number) {
    return PercentCalculator.doubleToPercent(doubleValue, 2);
  }
}
