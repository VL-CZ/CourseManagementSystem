import {TestQuestion} from './testQuestion';

export class CourseTestVM {
  id: number;
  topic: string;
  weight: number;
  questions: TestQuestion[];
}
