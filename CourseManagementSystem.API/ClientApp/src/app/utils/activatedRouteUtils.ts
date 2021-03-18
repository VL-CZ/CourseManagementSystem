import {ActivatedRoute} from '@angular/router';

/**
 * class containing additional {@link ActivatedRoute} methods
 */
export class ActivatedRouteUtils {
  /**
   * get id parameter value from the route
   * @param route route
   * @private
   * @returns id parameter value
   */
  public static getIdParam(route: ActivatedRoute): string {
    return this.getParam(route, 'id');
  }

  /**
   * get parameter value from the route
   * @param route route
   * @param paramName parameter name
   * @private
   * @returns parameter value
   */
  private static getParam(route: ActivatedRoute, paramName: string): string {
    return route.snapshot.paramMap.get(paramName);
  }
}
