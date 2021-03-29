export class PersonIdVM {
  /**
   * identifier of the person
   */
  id: string;
}

/**
 * viewmodel representing member of a course
 */
export class CourseMemberVM extends PersonIdVM {
  /**
   * name of the person
   */
  name: string;

  /**
   * user's email
   */
  email: string;
}
