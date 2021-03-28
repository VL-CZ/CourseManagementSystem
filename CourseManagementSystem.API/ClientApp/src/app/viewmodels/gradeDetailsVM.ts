/**
 * viewmodel representing details about a grade
 */
export class GradeDetailsVM {
  /**
   * id of the grade
   */
  id: number;

  /**
   * percentual value of the grade (0=0%, 1=100%)
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
}
