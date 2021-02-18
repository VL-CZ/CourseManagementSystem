import {TestQuestion} from './testQuestion';

export interface CourseTest {
  id: number;
  topic: string;
  questions: TestQuestion[];
}
