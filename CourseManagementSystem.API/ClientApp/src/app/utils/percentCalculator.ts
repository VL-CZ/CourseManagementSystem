export class PercentCalculator {
  /**
   * convert double value to percents with selected number of decimal digits
   * @param value double value to convert (0=0%, 1=100%)
   * @param decimalDigits number of decimal digits of the result
   */
  public static doubleToPercent(value: number, decimalDigits: number): number {
    if (decimalDigits < 0) {
      decimalDigits = 0;
    }

    value *= 100;
    const power = Math.pow(10, decimalDigits);
    return Math.round(value * power) / power;
  }
}
