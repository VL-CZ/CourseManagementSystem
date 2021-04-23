import {Component, Input, OnInit} from '@angular/core';
import {CourseMemberService} from '../course-member.service';
import {PercentCalculator} from '../utils/percentCalculator';

@Component({
  selector: 'app-student-average-score',
  templateUrl: './student-average-score.component.html',
  styleUrls: ['./student-average-score.component.css']
})
export class StudentAverageScoreComponent implements OnInit {

  /**
   * average score of the student
   */
  public averageScore: number;

  @Input()
  private courseMemberId: string;

  private courseMemberService: CourseMemberService;

  constructor(courseMemberService: CourseMemberService) {
    this.courseMemberService = courseMemberService;
  }

  ngOnInit() {
    this.courseMemberService.getAverageScore(this.courseMemberId).subscribe(average => {
      this.averageScore = average.value;
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
