/**
 * viewmodel representing request for enrollment to a given course
 */
export class EnrollmentRequestVM {
  /**
   * identifier of the enrollment request
   */
  public id: string;

  /**
   * name of the person that requested the enrollment
   */
  public personName: string;

  /**
   * email of the person that requested the enrollment
   */
  public personEmail: string;
}
