/**
 * viewmodel for adding a grade
 */
export class AddGradeVM {
  /**
   * percentual value of the grade (0=0%,1=100%)
   */
  percentualValue: number;

  /**
   * topic of the grade
   */
  topic: string;

  /**
   * comment to the grade provided by teacher
   */
  comment: string;

  /**
   * weight of the grade
   */
  weight: number;

  constructor() {
  }
}
