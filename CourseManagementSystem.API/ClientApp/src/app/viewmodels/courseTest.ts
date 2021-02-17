import {TestQuestion} from './testQuestion';

export interface CourseTest {
  id: number;
  questions: TestQuestion[];
}
