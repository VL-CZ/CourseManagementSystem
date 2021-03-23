import {GradeDetailsVM} from './gradeDetailsVM';

export interface PersonIdVM {
  id: string;
}

export interface Person extends PersonIdVM {
  name: string;
  email: string;
}

export interface Student extends Person {
  grades: GradeDetailsVM[];
}
