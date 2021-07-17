import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {InformationDialogComponent} from '../components/information-dialog/information-dialog.component';

/**
 * class used for managing information dialogs
 */
export class InformationDialogManager {

  public constructor(private bsModalRef: BsModalRef, private modalService: BsModalService) {
  }

  /**
   * display the dialog
   * @param text text in the dialog
   */
  public displayDialog(text: string): void {
    const initialState = {
      text: text
    };

    this.bsModalRef = this.modalService.show(InformationDialogComponent, {initialState});
  }
}
