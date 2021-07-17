import {ActivatedRoute, Router} from '@angular/router';

/**
 * class containing additional methods for router
 */
export class RouterTools {
  /**
   * reload current page
   * @param router router
   * @param activatedRoute current URL route
   */
  public static reloadPage(router: Router, activatedRoute: ActivatedRoute): void {
    router.routeReuseStrategy.shouldReuseRoute = () => false;
    router.onSameUrlNavigation = 'reload';
    router.navigate(['./'], {relativeTo: activatedRoute, queryParamsHandling: 'preserve'});
  }
}
