import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {ErrorsDictionaryVM} from '../viewmodels/apiErrorResponseVM';
import {ErrorDialogComponent} from '../components/error-dialog/error-dialog.component';

/**
 * This class is responsible displaying error dialogs
 */
export class ErrorDialogManager {
  public constructor(private bsModalRef: BsModalRef, private modalService: BsModalService) {
  }

  /**
   * display a modal with error messages
   * @param error error object
   */
  public displayError(error: any) {
    const errorVM: ErrorsDictionaryVM = error;
    let errorMessages: string[] = [];

    for (const key of Object.keys(errorVM.errors)) {
      errorMessages = errorMessages.concat(errorVM.errors[key]);
    }

    const initialState = {
      errors: errorMessages
    };
    this.bsModalRef = this.modalService.show(ErrorDialogComponent, {initialState});
    console.error(error);
  }
}
