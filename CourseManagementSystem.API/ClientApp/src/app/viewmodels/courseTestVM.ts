import {TestQuestionVM} from './testQuestionVM';

export class CourseTestVM {
  id: number;
  topic: string;
  weight: number;
  questions: TestQuestionVM[];
}
