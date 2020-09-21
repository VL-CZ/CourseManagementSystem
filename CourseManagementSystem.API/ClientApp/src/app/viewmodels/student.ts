import {Grade} from './grade';

export interface Person {
  id: string;
  name: string;
  email: string;
}

export interface Student extends Person {
  grades: Grade[];
}
