export class AddGradeVM {
  value: number;
  topic: string;
  comment: string;

  constructor(value: number, topic: string, comment: string) {
    this.value = value;
    this.topic = topic;
    this.comment = comment;
  }
}
