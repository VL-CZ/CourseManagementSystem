import {Observable} from 'rxjs';
import {ErrorDialogManager} from './errorDialogManager';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';

/**
 * helper class for {@link Observable}
 */
export class ObservableWrapper {
  private errorModalManager: ErrorDialogManager;
  private modalService: BsModalService;
  private bsModalRef: BsModalRef;

  public constructor(bsModalRef: BsModalRef, bsModalService: BsModalService) {
    this.modalService = bsModalService;
    this.bsModalRef = bsModalRef;
    this.errorModalManager = new ErrorDialogManager(this.bsModalRef, this.modalService);
  }

  /**
   * subscribe to the data and execute the action next, or show the error dialog
   * @param data data on which we call subscribe
   * @param next action that defines what to do after successful subscribe
   */
  public subscribeOrShowError<T>(data: Observable<T>, next ?: (value: T) => void): void {
    data.subscribe(next, err => this.errorModalManager.displayError(err));
  }
}
