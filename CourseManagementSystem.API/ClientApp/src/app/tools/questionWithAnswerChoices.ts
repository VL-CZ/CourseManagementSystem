/**
 * class representing test question with choice of answers
 */
export class QuestionWithAnswerChoices {
  /**
   * delimiter that separates choices of answers
   */
  public static readonly answerDelimiter = '@@@@';

  /**
   * delimiter that separates letter of the answer choice from the choice text
   */
  public static readonly answerLetterDelimiter = '@@@';

  /**
   * text of the question
   */
  public questionText: string;

  /**
   * choices of answers to the question
   */
  public answerChoices: AnswerChoice[];

  constructor(questionText: string, answerChoices: AnswerChoice[]) {
    this.questionText = questionText;
    this.answerChoices = answerChoices;
  }

  /**
   * create the instance from serialized string
   * <br>
   * expected format of the string: QUESTION TEXT followed by choices of answers separated by {@link answerDelimiter}
   * each choice of answers contains answer choice letter and its text separated by {@link answerLetterDelimiter}
   * @param serializedQuestionText
   */
  public static createFrom(serializedQuestionText: string): QuestionWithAnswerChoices {
    if (serializedQuestionText == null) {
      serializedQuestionText = '';
    }

    const questionParts = serializedQuestionText.split(QuestionWithAnswerChoices.answerDelimiter);

    if (questionParts.length === 0) {
      return new QuestionWithAnswerChoices('', []);
    }
    if (questionParts.length === 1) {
      return new QuestionWithAnswerChoices(questionParts[0], []);
    }

    // select answers
    const answers = questionParts.slice(1).map(answer => answer.split(this.answerLetterDelimiter))
      .map(answerParts => new AnswerChoice(answerParts[0], answerParts[1]));
    return new QuestionWithAnswerChoices(questionParts[0], answers);
  }

  /**
   * serialize the object to string
   * <br>
   * format of the serialized string: QUESTION TEXT followed by choices of answers separated by {@link answerDelimiter}
   * each choice of answers contains answer choice letter and its text separated by {@link answerLetterDelimiter}
   */
  public serialize(): string {
    const serializedAnswers = this.answerChoices.map(choice =>
      choice.answerLetter + QuestionWithAnswerChoices.answerLetterDelimiter + choice.answerText)
      .join(QuestionWithAnswerChoices.answerDelimiter);
    return [this.questionText, serializedAnswers].join(QuestionWithAnswerChoices.answerDelimiter);
  }
}

/**
 * class representing one answer from the answer choices
 */
export class AnswerChoice {
  /**
   * letter of the answer choice
   */
  public answerLetter: string;

  /**
   * text of the answer choice
   */
  public answerText: string;

  constructor(answerLetter: string, answerText: string) {
    this.answerLetter = answerLetter;
    this.answerText = answerText;
  }
}
