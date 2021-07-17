import {Component, Input, OnInit} from '@angular/core';
import {CourseMemberService} from '../../services/course-member.service';
import {QuizSubmissionInfoVM} from '../../viewmodels/quizSubmissionInfoVM';
import {Router} from '@angular/router';
import {PageNavigator} from '../../tools/pageNavigator';

@Component({
  selector: 'app-student-quiz-submissions',
  templateUrl: './student-quiz-submissions.component.html',
  styleUrls: ['./student-quiz-submissions.component.css']
})
export class StudentQuizSubmissionsComponent implements OnInit {

  @Input()
  private courseMemberId: string;

  /**
   * list of student's test submissions
   */
  public quizSubmissions: QuizSubmissionInfoVM[] = [];

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
    this.courseMemberService.getQuizSubmissions(this.courseMemberId).subscribe(submissions => {
      this.quizSubmissions = submissions;
    });
  }

}
