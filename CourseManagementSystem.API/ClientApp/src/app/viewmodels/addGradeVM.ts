/**
 * viewmodel for adding a grade
 */
export class AddGradeVM {
  /**
   * percentual value of the grade (0=0%,1=100%)
   */
  public percentualValue: number;

  /**
   * topic of the grade
   */
  public topic: string;

  /**
   * comment to the grade provided by teacher
   */
  public comment: string;

  /**
   * weight of the grade
   */
  public weight: number;

  constructor() {
  }
}
