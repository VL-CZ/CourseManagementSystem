/**
 * class for binding {@link Date} values
 */
export class DateTimeBinder {
  /**
   * date value
   */
  public date: string;

  /**
   * time value
   */
  public time: string;

  /**
   * get DateTime string
   */
  public toString(): string {
    const dateTimeString = `${this.date}T${this.time}`;
    return new Date(dateTimeString).toISOString();
  }
}
