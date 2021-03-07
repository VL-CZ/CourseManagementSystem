import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {CourseTestService} from '../course-test.service';
import {CourseTestVM} from '../viewmodels/courseTestVM';
import { TestQuestion } from '../viewmodels/testQuestion';
import { ArrayUtils } from '../utils/arrayUtils';

@Component({
  selector: 'app-test-create',
  templateUrl: './test-create.component.html',
  styleUrls: ['./test-create.component.css']
})
export class TestCreateComponent implements OnInit {
  private readonly courseId: string;
  private courseTestService: CourseTestService;
  private router: Router;

  public testToCreate: CourseTestVM;
  public questionCount = 0;

  constructor(route: ActivatedRoute, courseTestService: CourseTestService, router: Router) {
    this.courseId = route.snapshot.paramMap.get('id');
    this.courseTestService = courseTestService;
    this.router = router;
    this.testToCreate = new CourseTestVM();
    this.testToCreate.questions = [];
  }

  ngOnInit() {
  }

  public createTest(): void {
    this.courseTestService.addToCourse(this.testToCreate, this.courseId).subscribe(() => {
      this.router.navigate(['/courses', this.courseId]);
    });
  }

  public updateQuestionCount(): void {
    const questions = this.testToCreate.questions;
    const instance = new TestQuestion();
    ArrayUtils.resize<TestQuestion>(questions, this.questionCount, instance);

    this.setQuestionNumbers(questions);
  }

  private setQuestionNumbers(questions: TestQuestion[]): void {
    for (let i = 0; i < questions.length; i++) {
      const questionNumber = i + 1;
      questions[i].number = questionNumber;
    }
  }
}
