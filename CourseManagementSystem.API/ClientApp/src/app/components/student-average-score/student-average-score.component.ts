import {Component, Input, OnInit} from '@angular/core';
import {CourseMemberService} from '../../services/course-member.service';
import {PercentStringFormatter} from '../../tools/percent-tools/percentStringFormatter';

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

  /**
   * formatter of percent strings
   */
  public percentStringFormatter: PercentStringFormatter = new PercentStringFormatter();

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
}
