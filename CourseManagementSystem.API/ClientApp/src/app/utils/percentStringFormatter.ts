import {PercentCalculator} from './percentCalculator';

/**
 * class used for formatting Percent strings
 */
export class PercentStringFormatter {

  /**
   * format double value to percent string
   * @param doubleValue percentual value in double format (0->0%, 1->100%)
   * @param decimalDigits decimal digits to round
   */
  public format(doubleValue: number, decimalDigits: number = 2): string {
    return PercentCalculator.doubleToPercent(doubleValue, decimalDigits).toString() + '%';
  }
}
