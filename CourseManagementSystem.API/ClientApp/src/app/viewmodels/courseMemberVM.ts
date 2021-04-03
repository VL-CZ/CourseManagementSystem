export class PersonIdVM {
  /**
   * identifier of the person
   */
  public id: string;
}

/**
 * viewmodel representing member of a course
 */
export class CourseMemberVM extends PersonIdVM {
  /**
   * name of the person
   */
  public name: string;

  /**
   * user's email
   */
  public email: string;
}
