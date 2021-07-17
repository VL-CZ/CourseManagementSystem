import {Component, Input, OnInit} from '@angular/core';
import {CourseMemberService} from '../../services/course-member.service';
import {QuizSubmissionInfoVM} from '../../viewmodels/quizSubmissionInfoVM';

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

  private courseMemberService: CourseMemberService;

  constructor(courseMemberService: CourseMemberService) {
    this.courseMemberService = courseMemberService;
  }

  ngOnInit() {
    this.courseMemberService.getQuizSubmissions(this.courseMemberId).subscribe(submissions => {
      this.quizSubmissions = submissions;
    });
  }

}
