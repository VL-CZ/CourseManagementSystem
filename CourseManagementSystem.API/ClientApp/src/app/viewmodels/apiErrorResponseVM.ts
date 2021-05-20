/**
 * class representing dictionary of errors
 */
export class ErrorsDictionaryVM {
  /**
   * errors found
   *
   * in format {errorDescription}:{errorMessage(s)}
   */
  errors: { [key: string]: string[]; };
}

/**
 * class representing error response from API
 */
export class ApiErrorResponseVM {
  /**
   * error object of the response
   */
  error: ErrorsDictionaryVM;
}
