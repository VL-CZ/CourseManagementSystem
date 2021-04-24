export interface ErrorsDictionary {
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
export interface ValidationError {
  error: ErrorsDictionary;
}
