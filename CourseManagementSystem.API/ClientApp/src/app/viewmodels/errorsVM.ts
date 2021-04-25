export class ErrorsDictionary {
  /**
   * errors discovered during validation
   *
   * in format {fieldName}:{errorMessage}
   */
  errors: { [key: string]: string; };
}

/**
 * class representing error during data validation
 */
export class ValidationError {
  error: ErrorsDictionary;
}
